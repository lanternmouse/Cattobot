using System.Text;
using Cattobot.AutocompleteHandlers;
using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
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
    [SlashCommand("add", "Add a new film from Kinopoisk")]
    public async Task AddKinopoisk(
        [Autocomplete(typeof(KinopoiskAutocompleteHandler))] int query)
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
    public async Task List(IUser? user = null)
    {
        var guildId = Context.Guild.Id;

        var films = await filmRepo.GetGuildListQuery(guildId, user?.Id, [])
            .ToListAsync();

        var text = new StringBuilder();
        
        var index = 1;
        text.AppendLine("Запланировано:");
        foreach (var film in films.Where(x => x.FilmStatus == FilmStatus.Planned))
            text.AppendLine($"{index++}. {film.Film.LocalizedTitle} ({film.Film.Year})");
        text.AppendLine();

        index = 1;
        text.AppendLine("Просмотрено:");
        foreach (var film in films.Where(x => x.FilmStatus == FilmStatus.Completed))
            text.AppendLine($"{index++}. {film.Film.LocalizedTitle} ({film.Film.Year})");
        text.AppendLine();
        
        index = 1;
        text.AppendLine("Брошено:");
        foreach (var film in films.Where(x => x.FilmStatus == FilmStatus.Abandoned))
            text.AppendLine($"{index++}. {film.Film.LocalizedTitle} ({film.Film.Year})");
        text.AppendLine();

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text.ToString()));

        var title = "Список фильмов" + (string.IsNullOrEmpty(user?.Username) ? "" : (user!.Username + " ")) + ".txt";

        await RespondWithFileAsync(stream, title);
    }
    
    [SlashCommand("roll", "Get random film from list")]
    public async Task Roll()
    {
        var random = new Random(DateTime.UtcNow.Millisecond);
        
        var guildId = Context.Guild.Id;

        var filmsQuery = filmRepo.GetGuildListQuery(guildId, null, []);

        var filmCount = await filmsQuery.CountAsync();

        var pickedFilm = await filmsQuery
            .Skip(random.Next(0, filmCount - 1))
            .OrderBy(x => x.Id)
            .FirstAsync();

        await DeferAsync();

        var film = await kinopoiskFilmsClient.FilmsAsync(pickedFilm.Film.KinopoiskId!.Value);

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
        [Autocomplete(typeof(PlannedFilmsAutocompleteHandler))] string query
    )
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.RemoveGuildMember(id, Context.User.Id, Context.Guild.Id);

        await RespondAsync($"Фильм **[{film.LocalizedTitle}]** удалён из вашего списка");
    }
    
    [SlashCommand("mark-as-watched", "Marks film as watched")]
    public async Task MarkAsWatched(
        [Autocomplete(typeof(PlannedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Completed);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** помечен как просмотренный");
    }
}