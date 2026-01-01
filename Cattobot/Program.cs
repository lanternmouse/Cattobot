using System.Reflection;
using Cattobot.Automapper;
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

namespace Cattobot;

public class Program
{
    public static async Task Main()
    {
        var builder = Host.CreateApplicationBuilder();
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        builder.Services.Configure<CattobotOptions>(configuration.GetSection("Cattobot"));

        # region Entity Framework

        builder.Services.AddPooledDbContextFactory<CattobotDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Default")));
        builder.Services.AddDbContext<CattobotDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Default")));
        
        # endregion
        
        # region Discord
            
        builder.Services.AddSingleton(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All,
            LogLevel = LogSeverity.Verbose
        });
        builder.Services.AddSingleton<DiscordSocketClient>();
        builder.Services.AddSingleton(new InteractionServiceConfig
        {
            AutoServiceScopes = true
        });
        builder.Services.AddSingleton<InteractionService>(sp =>
            new InteractionService(
                sp.GetRequiredService<DiscordSocketClient>(),
                sp.GetRequiredService<InteractionServiceConfig>()
            ));
        
        # endregion

        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetExecutingAssembly());
        builder.Services.AddSingleton(config);
        builder.Services.AddMapster();
        
        builder.Services.AddKinopoiskIntegration(configuration);
        builder.Services.AddScoped<IFilmRepository, DbFilmRepository>();
        
        builder.Build();
        
        await using (var serviceProvider = builder.Services.BuildServiceProvider())
        {
            var db = serviceProvider.GetRequiredService<CattobotDbContext>();
            await db.Database.MigrateAsync();
        }

        await RunAsync();
        
        return;

        async Task RunAsync()
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            var interactionService = serviceProvider.GetRequiredService<InteractionService>();
            var config = serviceProvider.GetRequiredService<IOptions<CattobotOptions>>();
            var logger = serviceProvider.GetRequiredService<ILogger<DiscordSocketClient>>();
            
            client.Log += async (msg) =>
            {
                await Task.CompletedTask;
                Console.WriteLine(msg);
            };
            
            client.Ready += async () =>
            {
                await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

#if DEBUG
                await interactionService.RegisterCommandsToGuildAsync(983041476315459615, true);
                await interactionService.RegisterCommandsToGuildAsync(381045322044145679, true);
#else
                await interactionService.RegisterCommandsGloballyAsync(true);
#endif
            };

            client.InteractionCreated += async (x) =>
            {
                var ctx = new SocketInteractionContext(client, x);
                await interactionService.ExecuteCommandAsync(ctx, serviceProvider);
            };

            await client.LoginAsync(TokenType.Bot, config.Value.Token);
            await client.StartAsync();
            
            await Task.Delay(Timeout.Infinite);
        }
    }
}