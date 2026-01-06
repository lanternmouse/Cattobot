using Cattobot.Db.Models;
using Cattobot.Helpers;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.WebSocket;

namespace Cattobot.Services;

public class ButtonHandler(
    IFilmService filmService
    ) : IButtonHandler
{
    public async Task Handle(SocketMessageComponent component)
    {
        var command = component.Data.CustomId.Split("_");
        FilmDb filmDb;
        Guid filmId;
        switch (command[0])
        {
            case "filmAdd":
                var kinopoiskId = int.Parse(command[1]);

                await component.DeferAsync();
                
                filmDb = await filmService.AddFromKinopoisk(kinopoiskId, component.User.Id, component.GuildId!.Value, true);

                await component.DeleteOriginalResponseAsync();
                
                await component.Channel.SendMessageAsync(
                    $"<@{component.User.Id}> повторно добавляет фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** в список запланированных",
                    embed: EmbedBuilderProvider.GetShortFilmInfoEmbed(filmDb).Build(),
                    allowedMentions: AllowedMentions.None);
                
                break;
            case "markAsWatched":
                filmId = Guid.Parse(command[1]);

                filmDb = await filmService.MarkAsWatched(filmId, component.GuildId!.Value);

                await component.RespondAsync(
                    $"<@{component.User.Id}> отмечает фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** как **просмотренный**",
                    allowedMentions: AllowedMentions.None);

                break;
            case "markAsAbandoned":
                filmId = Guid.Parse(command[1]);

                filmDb = await filmService.MarkAsAbandoned(filmId, component.GuildId!.Value);

                await component.RespondAsync(
                    $"<@{component.User.Id}> отмечает фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** как **брошенный**",
                    allowedMentions: AllowedMentions.None);

                break;
        }
    }
}