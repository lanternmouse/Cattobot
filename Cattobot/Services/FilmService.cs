using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Exceptions;
using Cattobot.Services.Abstractions;
using Kinopoisk.Gateway;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cattobot.Services;

public class FilmService(
    IFilmsClient kinopoiskFilmsClient,
    IFilmRepository filmRepo,
    IMapper mapper,
    IMemoryCache cache
    ) : IFilmService
{
    public async Task<FilmDb> AddFromKinopoisk(int kinopoiskId, ulong userId, ulong guildId, bool overwrite = false)
    {
        if (!overwrite)
        {
            var existingFilm = await filmRepo.GetGuildListQuery(guildId, userId, [])
                .Where(x => x.Film.KinopoiskId == kinopoiskId)
                .FirstOrDefaultAsync();

            if (existingFilm != null)
            {
                if (existingFilm.FilmStatus != FilmStatus.Planned)
                {
                    throw new FilmAlreadyExistsAsNonPlannedException();
                }

                throw new FilmAlreadyExistsException();
            }
        }

        var cacheKey = $"kinopoisk-{kinopoiskId}";
        FilmDb filmDb;
        if (cache.TryGetValue(cacheKey, out FilmSearchResponse_films? cachedFilm) && cachedFilm != null)
        {
            filmDb = mapper.Map<FilmDb>(cachedFilm);
        }
        else
        {
            var film = await kinopoiskFilmsClient.FilmsAsync(kinopoiskId);
            filmDb = mapper.Map<FilmDb>(film!);
        }

        await filmRepo.Add(filmDb, userId, guildId, FilmStatus.Planned);
        
        return filmDb;
    }
}