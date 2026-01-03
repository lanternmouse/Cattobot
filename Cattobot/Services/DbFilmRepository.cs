using Cattobot.Db;
using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Services;

public class DbFilmRepository(
    CattobotDbContext dbContext
    ) : IFilmRepository
{
    public async Task<Guid> Add(FilmDb film, ulong userId, ulong guildId, 
        CancellationToken ct = default)
    {
        var filmDb = await dbContext.Films
            .Include(x => x.Guilds.Where(s => s.GuildId == guildId))
            .ThenInclude(x => x.Members)
            .FirstOrDefaultAsync(x => x.KinopoiskId == film.KinopoiskId, ct);
        
        if (filmDb == null)
        {
            filmDb = film;
            await dbContext.Films.AddAsync(filmDb, ct);
        }

        var guildState = filmDb.Guilds.FirstOrDefault(x => x.GuildId == guildId);
        if (guildState == null)
        {
            guildState = new FilmGuildDb
            {
                GuildId = guildId,
                FilmStatus = FilmStatus.Planned
            };
            filmDb.Guilds.Add(guildState);
        }

        var guildMember = guildState.Members.FirstOrDefault(x => x.UserId == userId);
        if (guildMember == null)
        {
            guildMember = new FilmGuildMemberDb
            {
                UserId = userId
            };
            guildState.Members.Add(guildMember);
        }
        
        await dbContext.SaveChangesAsync(ct);
        
        return filmDb.Id;
    }

    public IQueryable<FilmGuildDb> GetGuildListQuery(ulong guildId, ulong? userId, FilmStatus[] statuses, string? search)
    {
        var filmsQuery = dbContext.FilmGuilds
            .Include(x => x.Members)
            .Where(x => x.GuildId == guildId)
            .Where(g => !userId.HasValue || g.Members.Any(m => m.UserId == userId))
            .OrderByDescending(x => x.StatusOn)
            .AsQueryable();

        if (statuses.Length != 0)
            filmsQuery = filmsQuery.Where(x => statuses.Contains(x.FilmStatus));
        
        if(!string.IsNullOrEmpty(search))
            filmsQuery = filmsQuery.Where(x => EF.Functions.ILike(x.Film.LocalizedTitle, $"%{search}%"));

        return filmsQuery;
    }

    public async Task<FilmDb> Get(Guid id, CancellationToken ct = default)
    {
        var film = await dbContext.Films.FirstAsync(x => x.Id == id, ct);

        return film;
    }

    public async Task RemoveGuildMember(Guid id, ulong userId, ulong guildId, CancellationToken ct = default)
    {
        var guild = await dbContext.FilmGuilds
            .Where(x => x.FilmId == id && x.GuildId == guildId)
            .FirstAsync(ct);
        
        guild.Members.RemoveAll(x => x.UserId == userId);

        await dbContext.SaveChangesAsync(ct);
    }
    
    public async Task SetGuildStatus(Guid id, ulong guildId, FilmStatus status, CancellationToken ct = default)
    {
        await dbContext.FilmGuilds.Where(x => x.FilmId == id && x.GuildId == guildId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.FilmStatus, status)
                    .SetProperty(p => p.StatusOn, DateTime.UtcNow),
                ct);
    }
}