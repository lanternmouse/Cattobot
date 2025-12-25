using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot;

public class Program
{
    private static IServiceProvider _serviceProvider;
    
    static async Task Main(string[] args)
    {
        var collection = new ServiceCollection();

        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All,
            LogLevel = LogSeverity.Verbose
        };

        var interactionConfig = new InteractionServiceConfig
        {
            AutoServiceScopes = true
        };
        
        collection.AddSingleton(config);
        collection.AddSingleton<DiscordSocketClient>();
        collection.AddSingleton(interactionConfig);
        collection.AddSingleton<InteractionService>(sp =>
            new InteractionService(
                sp.GetRequiredService<DiscordSocketClient>(),
                sp.GetRequiredService<InteractionServiceConfig>()
                ));

        _serviceProvider = collection.BuildServiceProvider();

        await RunAsync(args);
        
        async Task RunAsync(string[] args)
        {
            var client = _serviceProvider.GetRequiredService<DiscordSocketClient>();
            var interactionService = _serviceProvider.GetRequiredService<InteractionService>();

            var token = "";
            
            client.Log += async (msg) =>
            {
                await Task.CompletedTask;
                Console.WriteLine(msg);
            };
            
            client.Ready += async () =>
            {
                await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);

#if Debug
                await interactionService.RegisterCommandsToGuildAsync(983041476315459615, true);
                await interactionService.RegisterCommandsToGuildAsync(381045322044145679, true);
#else
                await interactionService.RegisterCommandsGloballyAsync(true);
#endif
            };

            client.InteractionCreated += async (x) =>
            {
                var ctx = new SocketInteractionContext(client, x);
                await interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
            };

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            
            await Task.Delay(Timeout.Infinite);
        }
    }
}

