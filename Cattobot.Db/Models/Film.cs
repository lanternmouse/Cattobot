namespace Cattobot.Db.Models;

public record Film
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateOnly ReleaseDate { get; set; }
    public int Duration { get; set; }
    public float VoteAverage { get; set; }
    
    public bool IsWatched { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    
    public Guid? KinopoiskId { get; set; }
}