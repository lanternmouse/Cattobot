namespace Cattobot.Db.Models;

public class TrackDb
{
    public Guid Id { get; set; }

    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";

    public int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }
    public string? ArtistUrl { get; set; }
    public string ExternalUrl { get; set; } = "";

    public DateTime AddedOn { get; set; }
}