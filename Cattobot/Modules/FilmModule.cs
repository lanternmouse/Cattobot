using System.ComponentModel;
using System.Text;
using Cattobot.AutocompleteHandlers;
using Cattobot.Configuration;
using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Exceptions;
using Cattobot.Helpers;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cattobot.Modules;

[Group("film", "Film Commands")]
public class FilmModule(
    IFilmRepository filmRepo,
    IFilmService filmService,
    IOptions<FilmsOptions> options
    ) : InteractionModuleBase
{
    [SlashCommand("add", "Добавить фильм в список запланированных")]
    public async Task AddKinopoisk(
        [Autocomplete(typeof(KinopoiskAutocompleteHandler))] int query)
    {
        var userId = Context.User.Id;
        var guildId = Context.Guild.Id;

        FilmDb filmDb;
        try
        {
            filmDb = await filmService.AddFromKinopoisk(query, userId, guildId);
        }
        catch (FilmAlreadyExistsAsNonPlannedException)
        {
            await RespondAsync("Данный фильм уже был просмотрен ранее",
                ephemeral: true,
                components: new ComponentBuilder().WithButton("Всё равно добавить", $"filmAdd-{query}")
                    .Build());
            return;
        }
        catch (FilmAlreadyExistsException)
        {
            await RespondAsync("Фильм уже в вашем списке запланированных", ephemeral: true);
            return;
        }

        await RespondAsync(
            $"Добавлен фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** в список запланированных",
            [EmbedBuilderProvider.GetShortFilmInfoEmbed(filmDb).Build()]);
    }

    [SlashCommand("list", "Получить список добавленных фильмов")]
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
    
    [SlashCommand("roll", "Получить случайно выбранный фильм из запланированных")]
    public async Task Roll()
    {
        var random = new Random(DateTime.UtcNow.Millisecond);
        
        var guildId = Context.Guild.Id;

        var filmsQuery = filmRepo.GetGuildListQuery(guildId, null, [FilmStatus.Planned]);

        var filmCount = await filmsQuery.CountAsync();

        if (filmCount == 0)
        {
            await RespondAsync("Список запланированных фильмов пуст", ephemeral: true);
            return;
        }

        var pickedFilm = await filmsQuery
            .Skip(random.Next(0, filmCount - 1))
            .OrderBy(x => x.Id)
            .FirstAsync();

        var url = options.Value.KinopoiskWatchLink.Replace($"{{{nameof(FilmDb.KinopoiskId)}}}",
            pickedFilm.Film.KinopoiskId.ToString());
        await RespondAsync(
            $"🎲 Случайным образом выбран фильм **{FilmHelper.BuildTitleWithMarkdownUrl(pickedFilm.Film)}**",
            [EmbedBuilderProvider.GetFullFilmInfoEmbed(pickedFilm.Film).Build()],
            components: new ComponentBuilder()
                .WithButton("Смотреть", url: url, style: ButtonStyle.Link)
                .Build());
    }
    
    [SlashCommand("remove", "Убрать фильм из списка")]
    public async Task Remove(
        [Autocomplete(typeof(GuildMemberFilmsAutocompleteHandler))] string query
    )
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.RemoveGuildMember(id, Context.User.Id, Context.Guild.Id);

        await RespondAsync($"Фильм **[{film.LocalizedTitle}]** удалён из вашего списка");
    }
    
    [SlashCommand("mark-as-watched", "Пометить фильм как просмотренный")]
    public async Task MarkAsWatched(
        [Autocomplete(typeof(NonWatchedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Completed);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** просмотрен");
    }
    
    [SlashCommand("mark-as-planned", "Пометить фильм как запланированный")]
    public async Task MarkAsPlanned(
        [Autocomplete(typeof(NonPlannedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Planned);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** запланирован");
    }
    
    [SlashCommand("mark-as-abandoned", "Пометить фильм как брошенный")]
    public async Task MarkAsAbandoned(
        [Autocomplete(typeof(NonAbandonedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, Context.Guild.Id, FilmStatus.Abandoned);

        await RespondAsync($"Фильм **{film.LocalizedTitle}** брошен");
    }
}