using Discord.Interactions;

namespace DiscordBot.Modules;

[Group("film", "Film Commands")]
public class FilmModule : InteractionModuleBase
{
    [SlashCommand("add", "Add a new film")]
    public async Task AddFilm()
    {
        await RespondAsync("Not implemented yet");
    }
}