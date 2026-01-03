namespace Cattobot.Db.Models;

public record FilmDb
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string LocalizedTitle { get; set; } = "";
    public string ShortDescription { get; set; } = "";
    public string Description { get; set; } = "";
    public int Year { get; set; }
    public int Duration { get; set; }
    public float RatingImdb { get; set; }
    public string[] Genres { get; set; } = [];
    public string[] Countries { get; set; } = [];
    public bool IsSeries { get; set; }
    
    public string PreviewImageUrl { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    
    public int? KinopoiskId { get; set; }

    public List<FilmGuildDb> Guilds { get; set; } = [];
}