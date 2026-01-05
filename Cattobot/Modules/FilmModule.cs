using System.Text;
using Cattobot.AutocompleteHandlers;
using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Helpers;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Kinopoisk.Gateway;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cattobot.Modules;

[Group("film", "Film Commands")]
public class FilmModule(
    IFilmsClient kinopoiskFilmsClient,
    IFilmRepository filmRepo,
    IMapper mapper,
    IMemoryCache cache
    ) : InteractionModuleBase
{
    [SlashCommand("add", "Add a new film from Kinopoisk")]
    public async Task AddKinopoisk(
        [Autocomplete(typeof(KinopoiskAutocompleteHandler))] int query)
    {
        var addedBy = Context.User.Id;
        var guild = Context.Guild.Id;

        // autocomplete return only kinopoisk ids for now
        var cacheKey = $"kinopoisk-{query}";
        FilmDb filmDb;
        if (cache.TryGetValue(cacheKey, out FilmSearchResponse_films? cachedFilm) && cachedFilm != null)
        {
            filmDb = mapper.Map<FilmDb>(cachedFilm);
        }
        else
        {
            var film = await kinopoiskFilmsClient.FilmsAsync(query);
            filmDb = mapper.Map<FilmDb>(film!);
        }
        
        await filmRepo.Add(filmDb, addedBy, guild);

        var embed = EmbedBuilderProvider.GetShortFilmInfoEmbed(filmDb).Build();

        await RespondAsync(
            $"Добавлен фильм **[{filmDb.LocalizedTitle} ({filmDb.Year})]({KinopoiskHelper.BuildUrl(filmDb.KinopoiskId!.Value)})** в список запланированных",
            [embed]
        );
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

        var filmsQuery = filmRepo.GetGuildListQuery(guildId, null, [FilmStatus.Planned]);

        var filmCount = await filmsQuery.CountAsync();

        var pickedFilm = await filmsQuery
            .Skip(random.Next(0, filmCount - 1))
            .OrderBy(x => x.Id)
            .FirstAsync();

        var embed = EmbedBuilderProvider.GetFullFilmInfoEmbed(pickedFilm.Film).Build();

        await RespondAsync(
            $"🎲 Случайным образом выбран фильм **[{pickedFilm.Film.LocalizedTitle} ({pickedFilm.Film.Year})]({KinopoiskHelper.BuildUrl(pickedFilm.Film.KinopoiskId!.Value)})**",
            [embed]);
    }
    
    [SlashCommand("remove", "Remove film from list")]
    public async Task Remove(
        [Autocomplete(typeof(GuildMemberFilmsAutocompleteHandler))] string query
    )
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.RemoveGuildMember(id, Context.User.Id, Context.Guild.Id);

        await RespondAsync($"Фильм **[{film.LocalizedTitle}]** удалён из вашего списка");
    }
    
    [SlashCommand("mark-as-watched", "Marks film as watched")]
    public async Task MarkAsWatched(
        [Autocomplete(typeof(NonWatchedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Completed);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** помечен как просмотренный");
    }
    
    [SlashCommand("mark-as-planned", "Marks film as planned")]
    public async Task MarkAsPlanned(
        [Autocomplete(typeof(NonPlannedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Planned);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** помечен как запланированный");
    }
    
    [SlashCommand("mark-as-abandoned", "Marks film as abandoned")]
    public async Task MarkAsAbandoned(
        [Autocomplete(typeof(NonAbandonedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Abandoned);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** помечен как брошенный");
    }
}