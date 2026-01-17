namespace Cattobot.Db.Models;

public class TrackQueueDb
{
    public Guid Id { get; set; }
    public ulong GuildId { get; set; }
    public Guid? CurrentTrackId { get; set; }
    public TrackQueueItemDb? CurrentTrack { get; set; }
}