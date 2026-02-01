using System.Reflection;
using Cattobot.Configuration;
using Cattobot.Db;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using Cattobot.Services.Repositories;
using Cattobot.Wikidata.Gateway;
using Cattobot.Youtube.Gateway;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.ComponentInteractions;
using NetCord.Services.ComponentInteractions;
using Serilog;
using TMDbLib.Client;

namespace Cattobot;

public class Program
{
    private static IServiceProvider _serviceProvider = null!;

    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        _serviceProvider = host.Services.CreateScope().ServiceProvider;

        var db = _serviceProvider.GetRequiredService<CattobotDbContext>();
        await db.Database.MigrateAsync();

        host.AddModules(typeof(Program).Assembly);

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        # region Logging

        builder.UseSerilog((_, services, conf) =>
        {
            conf.MinimumLevel.Debug()
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console();
        });

        # endregion

        builder.ConfigureServices(services =>
        {
            services.AddMemoryCache();

            services.Configure<CattobotOptions>(configuration.GetSection("Cattobot"));
            services.Configure<FilmsOptions>(configuration.GetSection("Films"));
            services.Configure<TmdbOptions>(configuration.GetSection("Tmdb"));

            # region TMDB

            services.AddSingleton<TMDbClient>(x =>
            {
                var config = x.GetRequiredService<IOptions<TmdbOptions>>();
                var client = new TMDbClient(config.Value.Token)
                {
                    DefaultCountry = "RU",
                    DefaultLanguage = "ru",
                    DefaultImageLanguage = "ru"
                };
                return client;
            });

            # endregion

            # region Entity Framework

            services.AddDbContext<CattobotDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("Cattobot.Db"))
            );

            # endregion

            # region Discord

            services.AddDiscordGateway(o =>
            {
                o.Intents = GatewayIntents.Guilds | GatewayIntents.Guilds | GatewayIntents.GuildVoiceStates;
            })
            .AddApplicationCommands()
            .AddComponentInteractions<ButtonInteraction, ButtonInteractionContext>();

            # endregion

            # region Mapster

            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddMapster();

            # endregion

            services.AddYoutubeIntegration(configuration);
            services.AddWikidataIntegration(configuration);

            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<ITrackQueueRepository, TrackQueueRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<ITrackQueueService, TrackQueueService>();
            services.AddScoped<IWikidataService, WikidataService>();

            services.AddSingleton<IVoiceChatService, VoiceChatService>();
            services.AddSingleton<IMusicPlayerManager, MusicPlayerManager>();
            services.AddTransient<IMusicPlayer, MusicPlayer>();

            services.AddHostedService<WikidataBackgroundService>();
        });

        return builder;
    }
}
