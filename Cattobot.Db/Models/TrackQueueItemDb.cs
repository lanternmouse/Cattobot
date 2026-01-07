namespace Cattobot.Db.Models;

public record TrackQueueItemDb
{
    public Guid Id { get; set; }

    public Guid TrackQueueId { get; set; }
    public TrackQueueDb TrackQueue { get; set; } = null!;

    public Guid TrackId { get; set; }
    public TrackDb Track { get; set; } = null!;
    
    public Guid NextTrackId { get; set; }
    public TrackDb NextTrack { get; set; } = null!;

    public ulong UserId { get; set; }
    public DateTime AddedOn { get; set; }
}