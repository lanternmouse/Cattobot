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
    
    [SlashCommand("unload-slash", "Unload guild's slash commands")]
    public async Task UnloadSlash()
    {
        await Context.Guild.BulkOverwriteApplicationCommandsAsync([]);
        await RespondAsync("Done!");
    }
}