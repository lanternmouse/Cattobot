using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface IFilmService
{
    Task<FilmDb> AddFromKinopoisk(int kinopoiskId, ulong userId, ulong guildId, bool overwrite = false);
}