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
        var existingTrack = await dbContext.TrackDb
            .FirstOrDefaultAsync(x => x.ExternalUrl == trackDb.ExternalUrl, ct);

        if (existingTrack != null) return existingTrack.Id;
        
        await dbContext.TrackDb.AddAsync(trackDb, ct);
        await dbContext.SaveChangesAsync(ct);
        return trackDb.Id;
    }
}