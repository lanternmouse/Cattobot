using Cattobot.Db;
using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Services.Repositories;

public class TrackRepository(
    CattobotDbContext dbContext
    ) : ITrackRepository
{
    public async Task<Guid> Add(TrackDb trackDb, CancellationToken ct = default)
    {
        var existingTrackId = await dbContext.TrackDb.AsNoTracking()
            .Where(x => x.ExternalUrl == trackDb.ExternalUrl)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(ct);

        if (existingTrackId != Guid.Empty) return existingTrackId;
        
        await dbContext.TrackDb.AddAsync(trackDb, ct);
        await dbContext.SaveChangesAsync(ct);
        return trackDb.Id;
    }

    public async Task<List<Guid>> AddRange(List<TrackDb> trackDbs, CancellationToken ct = default)
    {
        var existingTracks = await dbContext.TrackDb.AsNoTracking()
            .Where(x => trackDbs.Select(t => t.ExternalUrl).Contains(x.ExternalUrl))
            .ToDictionaryAsync(x => x.ExternalUrl, x => x.Id, ct);

        foreach (var track in trackDbs)
        {
            if (existingTracks.TryGetValue(track.ExternalUrl, out var existingTrackId))
            {
                track.Id = existingTrackId;
            }
            else
            {
                dbContext.TrackDb.Add(track);
            }
        }

        await dbContext.SaveChangesAsync(ct);
        
        return trackDbs.Select(x => x.Id).ToList();
    }
}