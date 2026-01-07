using Cattobot.Db.Models.Enums;

namespace Cattobot.Db.Models;

public class TrackQueueDb
{
    public Guid Id { get; set; }
    public ulong GuildId { get; set; }
    public TrackQueueStatus Status { get; set; }
    public Guid CurrentTrackId { get; set; }
    public TrackQueueItemDb CurrentTrack { get; set; } = null!;
    public List<TrackQueueItemDb> Tracks { get; set; } = [];
}