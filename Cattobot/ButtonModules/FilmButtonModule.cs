using Cattobot.Helpers;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace Cattobot.ButtonModules;

public class FilmButtonModule(IFilmService filmService) : ComponentInteractionModule<ButtonInteractionContext>
{
    [ComponentInteraction("filmAdd")]
    public async Task Add(int kinopoiskId)
    {
        var filmDb = await filmService.AddFromKinopoisk(kinopoiskId, Context.User.Id, Context.Guild!.Id, true);

        await Context.Message.DeleteAsync();

        await Context.Channel.SendMessageAsync(
            new MessageProperties()
                .WithContent(
                    $"<@{Context.User.Id}> повторно добавляет фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** в список запланированных")
                .WithEmbeds([EmbedPropertiesProvider.GetShortFilmInfoEmbed(filmDb)])
                .WithAllowedMentions(new AllowedMentionsProperties().WithAllowedUsers([])));
    }
    
    [ComponentInteraction("filmMarkAsWatched")]
    public async Task<InteractionMessageProperties> MarkAsCompleted(string filmId)
    {
        var guid = Guid.Parse(filmId);
        var filmDb = await filmService.MarkAsWatched(guid, Context.Guild!.Id);

        return new InteractionMessageProperties()
            .WithContent($"<@{Context.User.Id}> отмечает фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** как **просмотренный**")
            .WithFlags(MessageFlags.Ephemeral);
    }
    
    [ComponentInteraction("filmMarkAsAbandoned")]
    public async Task<InteractionMessageProperties> MarkAsAbandoned(string filmId)
    {
        var guid = Guid.Parse(filmId);
        var filmDb = await filmService.MarkAsAbandoned(guid, Context.Guild!.Id);

        return new InteractionMessageProperties()
            .WithContent($"<@{Context.User.Id}> отмечает фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** как **брошенный**")
            .WithAllowedMentions(new AllowedMentionsProperties().WithAllowedUsers([]));
    }
}