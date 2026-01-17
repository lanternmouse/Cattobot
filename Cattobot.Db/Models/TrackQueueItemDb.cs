namespace Cattobot.Db.Models;

public record TrackQueueItemDb
{
    public Guid Id { get; set; }

    public Guid QueueId { get; set; }
    public TrackQueueDb Queue { get; set; } = null!;

    public Guid TrackId { get; set; }
    public TrackDb Track { get; set; } = null!;
    
    public Guid? PrevItemId { get; set; }
    public TrackQueueItemDb? PrevItem { get; set; } = null!;
    
    public Guid? NextItemId { get; set; }
    public TrackQueueItemDb? NextItem { get; set; } = null!;

    public ulong UserId { get; set; }
    public DateTime AddedOn { get; set; }
}