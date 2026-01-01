using System.Text;
using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Kinopoisk.Gateway;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Modules;

[Group("film", "Film Commands")]
public class FilmModule(
    IFilmsClient kinopoiskFilmsClient,
    IFilmRepository filmRepo,
    IMapper mapper
    ) : InteractionModuleBase
{
    [SlashCommand("add", "Add a new film")]
    public async Task Add(
        [Autocomplete(typeof(AddFilmAutocompleteHandler))] int query)
    {
        var addedBy = Context.User.Id;
        var guild = Context.Guild.Id;

        await DeferAsync();
        
        // autocomplete return only kinopoisk ids for now
        var film = await kinopoiskFilmsClient.FilmsAsync(query);
        var filmDb = mapper.Map<FilmDb>(film!);
        await filmRepo.Add(filmDb, addedBy, guild);
        
        await FollowupAsync($"Добавлен фильм **[{film.NameRu} ({film.Year})]({film.WebUrl})**", [
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

    [SlashCommand("list", "Get list of films")]
    public async Task List(IUser? user)
    {
        var guildId = Context.Guild.Id;
        var films = await filmRepo.GetListQuery(guildId, user?.Id)
            .OrderBy(x => x.AddedOn)
            .ToListAsync();

        var text = new StringBuilder();
        var index = 1;
        foreach (var film in films)
            text.AppendLine($"{index++}. {film.LocalizedTitle} ({film.Year})");

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text.ToString()));

        var title = "Список фильмов" + (string.IsNullOrEmpty(user?.Username) ? "" : (user!.Username + " ")) + ".txt";

        await RespondWithFileAsync(stream, title);
    }
    
    [SlashCommand("roll", "Get random film from list")]
    public async Task Roll(
        [Autocomplete(typeof(FilmsAutocompleteHandler))] Guid query
        )
    {
        var random = new Random(DateTime.UtcNow.Millisecond);
        
        var guildId = Context.Guild.Id;

        var filmsQuery = filmRepo.GetListQuery(guildId, null);

        var filmCount = await filmsQuery.CountAsync();

        var pickedFilm = await filmsQuery
            .Skip(random.Next(0, filmCount - 1))
            .OrderBy(x => x.Id)
            .FirstAsync();

        await DeferAsync();

        var film = await kinopoiskFilmsClient.FilmsAsync(pickedFilm.KinopoiskId!.Value);

        await FollowupAsync($"Случайным образом выбран фильм **[{film.NameRu} ({film.Year})]({film.WebUrl})**", [
            new EmbedBuilder
            {
                ImageUrl = film.CoverUrl,
                Description = film.Description,
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
    
    [SlashCommand("remove", "Remove film from list")]
    public async Task Remove(
        [Autocomplete(typeof(FilmsAutocompleteHandler))] Guid query
    )
    {
        var film = await filmRepo.Get(query);
        await filmRepo.Remove(query);

        await FollowupAsync($"Фильм {film.LocalizedTitle} удалён из вашего списка");
    }
}

public class AddFilmAutocompleteHandler(
    IFilmsClient kinopoiskFilmsClient
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();
        if (value == null || value.Length < 3)
            return AutocompletionResult.FromSuccess([]);
        
        var filmSuggestions = await kinopoiskFilmsClient.SearchByKeywordAsync(value, 1);

        var results = filmSuggestions.Films.Select(s => new AutocompleteResult(
            $"{s.NameRu} ({s.Year}), {s.NameEn}",
            s.FilmId
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}

public class FilmsAutocompleteHandler(
    IFilmRepository filmRepo
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();

        var filmSuggestions = await filmRepo.GetListQuery(context.Guild.Id, context.User.Id)
            .Where(x => EF.Functions.ILike(x.LocalizedTitle, $"%{value}%"))
            .OrderByDescending(x => x.AddedOn)
            .Take(25)
            .ToListAsync();

        var results = filmSuggestions.Select(s => new AutocompleteResult(
            $"{s.LocalizedTitle} ({s.Year}), {s.Title}",
            s.Id
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}