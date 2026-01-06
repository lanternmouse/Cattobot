using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface IFilmService
{
    Task<FilmDb> AddFromKinopoisk(int kinopoiskId, ulong userId, ulong guildId, bool overwrite = false);

    Task<FilmDb> PickRandom(ulong guildId);

    Task<FilmDb> MarkAsWatched(Guid id, ulong guildId);

    Task<FilmDb> MarkAsPlanned(Guid id, ulong guildId);

    Task<FilmDb> MarkAsAbandoned(Guid id, ulong guildId);
}