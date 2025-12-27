using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using MapsterMapper;

namespace Cattobot.Modules;

[Group("film", "Film Commands")]
public class FilmModule(
    IFilmRepository filmRepo,
    IMapper mapper
    ) : InteractionModuleBase
{
    [SlashCommand("add", "Add a new film")]
    public async Task AddFilm(
        [Autocomplete(typeof(AddFilmAutocompleteHandler))] int query)
    {
        var addedBy = Context.User.Id;
        var guild = Context.Guild.Id;
        
        // autocomplete return only kinopoisk ids for now
        var film = await filmRepo.GetFilmFromKinopoisk(query);
        await filmRepo.Add(mapper.Map<FilmDb>(film), addedBy, guild);
        
        await RespondAsync($"<@{addedBy}> добавляет фильм **[{film.NameRu} ({film.Year})]({film.WebUrl})**", [
            new EmbedBuilder
            {
                ThumbnailUrl = film.PosterUrlPreview,
                Description = film.ShortDescription,
                Fields =
                [
                    new EmbedFieldBuilder()
                    {
                        Name = "Страна",
                        IsInline = true,
                        Value = string.Join(", ", film.Countries.Select(c => c.Country1))
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Жанр",
                        IsInline = true,
                        Value = string.Join(", ", film.Genres.Select(g => g.Genre1))
                    },
                    new EmbedFieldBuilder
                    {
                        Name = "Длительность",
                        IsInline = true,
                        Value = film.FilmLength.HasValue
                            ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(film.FilmLength.Value)).ToString("HH:mm")
                            : "Неизвестно"
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Рейтинг IMDB",
                        IsInline = true,
                        Value = film.RatingImdb.ToString() ?? "-"
                    }
                ]
            }.Build()
        ]);
    }
}

public class AddFilmAutocompleteHandler(
    IFilmRepository filmRepo
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();
        if (value == null || value.Length < 3)
            return AutocompletionResult.FromSuccess([]);
        
        var filmSuggestions = await filmRepo.GetSuggestionsFromKinopoisk(value, 1);

        var results = filmSuggestions.Select(s => new AutocompleteResult(
            $"{s.NameRu} ({s.Year}), {s.NameEn}",
            s.FilmId
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}