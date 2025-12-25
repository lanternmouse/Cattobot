using Discord.Interactions;

namespace DiscordBot.Modules;

public class DebugModule : InteractionModuleBase
{
    [SlashCommand("ping", "Check availability")]
    public async Task Ping()
    {
        await RespondAsync("Pong!");
    }
}