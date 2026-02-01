using Cattobot.Db.Models;
using YoutubeDLSharp.Metadata;

namespace Cattobot.Services.Abstractions;

public interface ITrackQueueService
{
    Task<(TrackQueueItemDb item, VideoData videoData)> EnqueueFromQuery(ulong guildId, ulong userId, string query, CancellationToken ct = default);
}