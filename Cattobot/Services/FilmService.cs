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

    public async Task<FilmDb> PickRandom(ulong guildId)
    {
        var random = new Random(DateTime.UtcNow.Millisecond);

        var participantsQuery = filmRepo.GetGuildListQuery(guildId, null, [FilmStatus.Planned])
            .Include(x => x.Members)
            .SelectMany(x => x.Members)
            .GroupBy(x => x.UserId)
            .Select(x => x.First().UserId);
        
        var participantsCount = await participantsQuery.CountAsync();
        
        if (participantsCount == 0)
        {
            throw new EmptyFilmListException();
        }

        var pickedParticipantId = await participantsQuery
            .OrderBy(x => x)
            .Skip(random.Next(0, participantsCount - 1))
            .FirstAsync();
        
        var filmsQuery = filmRepo.GetGuildListQuery(guildId, pickedParticipantId, [FilmStatus.Planned]);

        var filmCount = await filmsQuery.CountAsync();

        var pickedFilm = await filmsQuery
            .OrderBy(x => x.Id)
            .Skip(random.Next(0, filmCount - 1))
            .FirstAsync();

        return pickedFilm.Film;
    }

    public async Task<FilmDb> MarkAsWatched(Guid id, ulong guildId)
    {
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, guildId, FilmStatus.Completed);
        return film;
    }
    
    public async Task<FilmDb> MarkAsPlanned(Guid id, ulong guildId)
    {
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, guildId, FilmStatus.Planned);
        return film;
    }
    
    public async Task<FilmDb> MarkAsAbandoned(Guid id, ulong guildId)
    {
        var film = await filmRepo.Get(id);
        await filmRepo.SetGuildStatus(id, guildId, FilmStatus.Abandoned);
        return film;
    }
}