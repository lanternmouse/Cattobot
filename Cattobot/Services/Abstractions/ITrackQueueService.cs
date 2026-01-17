using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface ITrackQueueService
{
    Task<TrackDb> EnqueueFromQuery(ulong guildId, ulong userId, string query, CancellationToken ct = default);
}