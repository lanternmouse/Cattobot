using System.Text;
using Cattobot.AutocompleteHandlers;
using Cattobot.Configuration;
using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Exceptions;
using Cattobot.Helpers;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Cattobot.CommandModules;

[SlashCommand("film", "Film Commands")]
public class FilmCommandModule(
    IFilmRepository filmRepo,
    IFilmService filmService,
    IOptions<FilmsOptions> options
    ) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SubSlashCommand("add", "Добавить фильм в список запланированных")]
    public async Task<InteractionMessageProperties> AddKinopoisk(
        [SlashCommandParameter(AutocompleteProviderType = typeof(KinopoiskAutocompleteProvider))] int query)
    {
        var userId = Context.User.Id;
        var guildId = Context.Guild!.Id;

        FilmDb filmDb;
        try
        {
            filmDb = await filmService.AddFromKinopoisk(query, userId, guildId);
        }
        catch (FilmAlreadyExistsAsNonPlannedException)
        {
            return new InteractionMessageProperties()
                .WithContent("Данный фильм уже был просмотрен ранее")
                .WithFlags(MessageFlags.Ephemeral)
                .WithComponents([
                        new ActionRowProperties()
                        {
                            new ButtonProperties($"filmAdd:{query}", "Всё равно добавить", ButtonStyle.Primary)
                        }
                    ]
                );
        }
        catch (FilmAlreadyExistsException)
        {
            return new InteractionMessageProperties()
                .WithContent("Фильм уже в вашем списке запланированных")
                .WithFlags(MessageFlags.Ephemeral);
        }

        return new InteractionMessageProperties()
            .WithContent($"Добавлен фильм **{FilmHelper.BuildTitleWithMarkdownUrl(filmDb)}** в список запланированных")
            .WithEmbeds([
                EmbedPropertiesProvider.GetShortFilmInfoEmbed(filmDb)
            ]);
    }

    [SubSlashCommand("list", "Получить список добавленных фильмов")]
    public async Task<InteractionMessageProperties> List(User? user = null)
    {
        var guildId = Context.Guild!.Id;
    
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
    
        var title = "Список фильмов" + (string.IsNullOrEmpty(user?.Username) ? "" : (" " + user!.Username)) + ".txt";

        return new InteractionMessageProperties()
            .WithAttachments([new AttachmentProperties(title, stream)]);
    }
    
    [SubSlashCommand("roll", "Получить случайно выбранный фильм из запланированных")]
    public async Task<InteractionMessageProperties> Roll()
    {
        FilmDb film;
        try
        {
            film = await filmService.PickRandom(Context.Guild!.Id);
        }
        catch (EmptyFilmListException)
        {
            return new InteractionMessageProperties()
                .WithContent("Список запланированных фильмов пуст")
                .WithFlags(MessageFlags.Ephemeral);
        }
    
        var url = options.Value.KinopoiskWatchLink.Replace($"{{{nameof(FilmDb.KinopoiskId)}}}",
            film.KinopoiskId.ToString());

        return new InteractionMessageProperties()
            .WithContent($"🎲 Случайным образом выбран фильм **{FilmHelper.BuildTitleWithMarkdownUrl(film)}**")
            .WithEmbeds([EmbedPropertiesProvider.GetFullFilmInfoEmbed(film)])
            .WithComponents([
                new ActionRowProperties
                {
                    new LinkButtonProperties(url, "Смотреть"),
                    new ButtonProperties($"filmMarkAsWatched:{film.Id}", "В просмотренные", ButtonStyle.Primary),
                    new ButtonProperties($"filmMarkAsAbandoned:{film.Id}", "В брошенные", ButtonStyle.Secondary),
                }
            ]);
    }
    
    [SubSlashCommand("remove", "Убрать фильм из списка")]
    public async Task<InteractionMessageProperties> Remove(
        [SlashCommandParameter(AutocompleteProviderType = typeof(KinopoiskAutocompleteProvider))] string query
    )
    {
        var id = Guid.Parse(query);
        
        var film = await filmRepo.Get(id);
        await filmRepo.RemoveGuildMember(id, Context.User.Id, Context.Guild!.Id);
        
        return new InteractionMessageProperties().WithContent(
            $"Фильм **{FilmHelper.BuildTitleWithMarkdownUrl(film)}** удалён из вашего списка");
    }
    
    [SubSlashCommand("mark-as-watched", "Пометить фильм как просмотренный")]
    public async Task<InteractionMessageProperties> MarkAsWatched(
        [SlashCommandParameter(AutocompleteProviderType = typeof(NonWatchedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
    
        var film = await filmService.MarkAsWatched(id, Context.Guild!.Id);

        return new InteractionMessageProperties().WithContent(
            $"Фильм **{FilmHelper.BuildTitleWithMarkdownUrl(film)}** отмечен как **просмотренный**");
    }
    
    [SubSlashCommand("mark-as-planned", "Пометить фильм как запланированный")]
    public async Task<InteractionMessageProperties> MarkAsPlanned(
        [SlashCommandParameter(AutocompleteProviderType = typeof(NonPlannedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
        
        var film = await filmService.MarkAsPlanned(id, Context.Guild!.Id);
    
        return new InteractionMessageProperties().WithContent(
            $"Фильм **{FilmHelper.BuildTitleWithMarkdownUrl(film)}** отмечен как **запланированный**");
    }
    
    [SubSlashCommand("mark-as-abandoned", "Пометить фильм как брошенный")]
    public async Task<InteractionMessageProperties> MarkAsAbandoned(
        [SlashCommandParameter(AutocompleteProviderType = typeof(NonAbandonedFilmsAutocompleteHandler))] string query)
    {
        var id = Guid.Parse(query);
    
        var film = await filmService.MarkAsAbandoned(id, Context.Guild!.Id);

        return new InteractionMessageProperties().WithContent(
            $"Фильм **{FilmHelper.BuildTitleWithMarkdownUrl(film)}** отмечен как **брошенный**");
    }
}