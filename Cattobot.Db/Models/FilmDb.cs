namespace Cattobot.Db.Models;

public record FilmDb
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string LocalizedTitle { get; set; } = "";
    public string Description { get; set; } = "";
    public int Year { get; set; }
    public int Duration { get; set; }
    public float RatingImdb { get; set; }
    
    public bool IsWatched { get; set; }
    public bool IsDeleted { get; set; }
    
    public ulong GuildId { get; set; }
    public ulong AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    
    public int? KinopoiskId { get; set; }
}