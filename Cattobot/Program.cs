using System.Reflection;
using Cattobot.Configuration;
using Cattobot.Db;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Kinopoisk.Gateway;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

namespace Cattobot;

public class Program
{
    private static IServiceProvider _serviceProvider = null!;

    public static async Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args).Build();

        _serviceProvider = builder.Services.CreateScope().ServiceProvider;

        var db = _serviceProvider.GetRequiredService<CattobotDbContext>();
        await db.Database.MigrateAsync();

        await RunAsync();
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

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        builder.UseSerilog();
        
        # endregion

        builder.ConfigureServices(services =>
        {
            services.Configure<CattobotOptions>(configuration.GetSection("Cattobot"));

            # region Entity Framework
            
            services.AddDbContext<CattobotDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("Cattobot.Db"))
            );

            # endregion

            # region Discord

            services.AddSingleton(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All,
                LogLevel = LogSeverity.Verbose,
            });
            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton(new InteractionServiceConfig
            {
                AutoServiceScopes = true
            });
            services.AddSingleton<InteractionService>(sp =>
                new InteractionService(
                    sp.GetRequiredService<DiscordSocketClient>(),
                    sp.GetRequiredService<InteractionServiceConfig>()
                ));

            # endregion

            # region Mapster

            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddMapster();

            # endregion

            services.AddKinopoiskIntegration(configuration);
            services.AddScoped<IFilmRepository, DbFilmRepository>();
        });

        return builder;
    }

    private static async Task RunAsync()
    {
        var client = _serviceProvider.GetRequiredService<DiscordSocketClient>();
        var interactionService = _serviceProvider.GetRequiredService<InteractionService>();
        var config = _serviceProvider.GetRequiredService<IOptions<CattobotOptions>>();
        var logger = _serviceProvider.GetRequiredService<ILogger<DiscordSocketClient>>();

        client.Log += LogAsync;

        client.Ready += async () =>
        {
            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);

#if DEBUG
            await interactionService.RegisterCommandsToGuildAsync(983041476315459615, true);
#else
            await interactionService.RegisterCommandsGloballyAsync(true);
#endif
        };

        client.InteractionCreated += async (x) =>
        {
            var ctx = new SocketInteractionContext(client, x);
            await interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
        };

        await client.LoginAsync(TokenType.Bot, config.Value.Token);
        await client.StartAsync();

        await Task.Delay(Timeout.Infinite);
    }
    
    private static async Task LogAsync(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };
        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        await Task.CompletedTask;
    }
}