using Cattobot.Db;
using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Services;

public class DbFilmRepository(
    CattobotDbContext dbContext
    ) : IFilmRepository
{
    public async Task<Guid> Add(FilmDb film, ulong addedBy, ulong guildId, CancellationToken ct = default)
    {
        var filmDb = await dbContext.Films
            .FirstOrDefaultAsync(x => x.KinopoiskId == film.KinopoiskId && x.GuildId == guildId && x.AddedBy == addedBy,
                ct);

        if (filmDb == null)
        {
            film.AddedOn = DateTime.UtcNow;
            await dbContext.Films.AddAsync(film, ct);
        }
        else
        {
            filmDb = film with
            {
                AddedOn = filmDb.AddedOn,
            };
        }
        
        film.AddedBy = addedBy;
        film.GuildId = guildId;
        
        await dbContext.SaveChangesAsync(ct);
        
        return film.Id;
    }

    public IQueryable<FilmDb> GetListQuery(ulong guildId, ulong? userId)
    {
        var filmsQuery = dbContext.Films
            .Where(x => x.GuildId == guildId)
            .Where(x => !userId.HasValue || x.AddedBy == userId.Value)
            .GroupBy(x => x.KinopoiskId)
            .Select(x => x.First());

        return filmsQuery;
    }

    public async Task<FilmDb> Get(Guid id, CancellationToken ct = default)
    {
        var film = await dbContext.Films.FirstAsync(x => x.Id == id, ct);

        return film;
    }

    public async Task Remove(Guid id, CancellationToken ct = default)
    {
        await dbContext.Films.Where(x => x.Id == id).ExecuteDeleteAsync(ct);
    }
    
    public async Task MarkWatched(Guid id, CancellationToken ct = default)
    {
        await dbContext.Films.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsWatched, true), ct);
    }
}