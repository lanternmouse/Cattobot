using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface ITrackQueueService
{
    Task<TrackQueueItemDb> EnqueueFromQuery(ulong guildId, ulong userId, string query, CancellationToken ct = default);
}