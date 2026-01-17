using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface ITrackQueueRepository
{
    Task<TrackQueueDb> GetOrCreate(ulong guildId, CancellationToken ct = default);

    Task<Guid> Append(Guid queueId, Guid trackId, ulong userId, CancellationToken ct = default);

    Task<TrackQueueItemDb?> GetItem(Guid itemId, CancellationToken ct = default);
    
    Task<TrackQueueItemDb?> GetLastItem(Guid queueId, CancellationToken ct = default);

    Task<TrackQueueItemDb?> GetFirstItem(Guid queueId, CancellationToken ct = default);

    Task<TrackQueueItemDb?> GetCurrentItem(Guid queueId, CancellationToken ct = default);

    Task SetCurrentItem(Guid queueId, Guid? trackItemId, CancellationToken ct = default);

    Task Drop(ulong guildId, CancellationToken ct = default);
}