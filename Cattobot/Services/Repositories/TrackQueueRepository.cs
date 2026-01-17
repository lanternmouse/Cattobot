using Cattobot.Db;
using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Services.Repositories;

public class TrackQueueRepository(
    CattobotDbContext dbContext
    ) : ITrackQueueRepository
{
    public async Task<TrackQueueDb> GetOrCreate(ulong guildId, CancellationToken ct = default)
    {
        var queue = await dbContext.TrackQueueDb.AsNoTracking()
            .FirstOrDefaultAsync(x => x.GuildId == guildId, ct);

        if (queue == null)
        {
            queue = new TrackQueueDb
            {
                GuildId = guildId
            };
            await dbContext.TrackQueueDb.AddAsync(queue, ct);
            await dbContext.SaveChangesAsync(ct);
        }

        return queue;
    }
    
    public async Task<Guid> Append(Guid queueId, Guid trackId, ulong userId, CancellationToken ct = default)
    {
        var lastItem = await GetLastItem(queueId, ct);

        var id = Guid.CreateVersion7();
        await dbContext.TrackQueueItemDb.AddAsync(new TrackQueueItemDb
        {
            Id = id,
            TrackId = trackId,
            QueueId = queueId,
            PrevItemId = lastItem?.Id,
            NextItemId = null,
            AddedOn = DateTime.UtcNow,
            UserId = userId
        }, ct);
        
        lastItem?.NextItemId = id;

        await dbContext.SaveChangesAsync(ct);

        if (lastItem != null)
            await dbContext.TrackQueueItemDb
                .Where(x => x.Id == lastItem.Id)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.NextItemId, id), ct);

        return id;
    }

    public async Task<TrackQueueItemDb?> GetItem(Guid itemId, CancellationToken ct = default)
    {
        return await dbContext.TrackQueueItemDb.AsNoTracking()
            .Include(x => x.Track)
            .FirstOrDefaultAsync(x => x.Id == itemId, ct);
    }
    
    public async Task<TrackQueueItemDb?> GetLastItem(Guid queueId, CancellationToken ct = default)
    {
        return await dbContext.TrackQueueItemDb.AsNoTracking()
            .Include(x => x.Track)
            .FirstOrDefaultAsync(x => x.QueueId == queueId && x.NextItemId == null, ct);
    }
    
    public async Task<TrackQueueItemDb?> GetFirstItem(Guid queueId, CancellationToken ct = default)
    {
        return await dbContext.TrackQueueItemDb.AsNoTracking()
            .Include(x => x.Track)
            .FirstOrDefaultAsync(x => x.QueueId == queueId && x.PrevItemId == null, ct);
    }
    
    public async Task<TrackQueueItemDb?> GetCurrentItem(Guid queueId, CancellationToken ct = default)
    {
        return await dbContext.TrackQueueDb.AsNoTracking()
            .Include(x => x.CurrentTrack)
            .ThenInclude(x => x!.Track)
            .Where(x => x.Id == queueId)
            .Select(x => x.CurrentTrack)
            .FirstOrDefaultAsync(ct);
    }

    public async Task SetCurrentItem(Guid queueId, Guid? trackItemId, CancellationToken ct = default)
    {
        await dbContext.TrackQueueDb
            .Where(x => x.Id == queueId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.CurrentTrackId, trackItemId), ct);
    }
    
    public async Task Drop(ulong guildId, CancellationToken ct = default)
    {
        await dbContext.TrackQueueDb.Where(x => x.GuildId == guildId).ExecuteDeleteAsync(ct);
    }
}