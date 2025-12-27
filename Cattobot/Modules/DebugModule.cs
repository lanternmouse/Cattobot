using Discord.Interactions;
using Discord.WebSocket;

namespace Cattobot.Modules;

public class DebugModule(
    DiscordSocketClient client
    ) : InteractionModuleBase
{
    [SlashCommand("ping", "Check availability")]
    public async Task Ping()
    {
        await RespondAsync($"Pong! Latency is {client.Latency} ms");
    }
}