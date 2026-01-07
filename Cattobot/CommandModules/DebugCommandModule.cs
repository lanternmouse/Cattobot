using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Cattobot.CommandModules;

public class DebugCommandModule() : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("ping", "Check availability")]
    public string Ping()
    {
        return $"Pong! Latency is {Context.Client.Latency.TotalMilliseconds} ms";
    }
    
    // [SlashCommand("unload-slash", "Unload guild's slash commands")]
    // public async Task UnloadSlash()
    // {
    //     await Context.Guild.BulkOverwriteApplicationCommandsAsync();
    //     await RespondAsync("Done!");
    // }
}