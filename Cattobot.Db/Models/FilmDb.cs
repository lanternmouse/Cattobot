namespace Cattobot.Db.Models;

public record FilmDb
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public int? Year { get; set; }
    public string Description { get; set; } = "";
    public int? Duration { get; set; }
    public float Rating { get; set; }
    public string[] Genres { get; set; } = [];
    public string[] Countries { get; set; } = [];
    public string[] Actors { get; set; } = [];
    public string[] Directors { get; set; } = [];
    public DateOnly? ReleaseDate { get; set; }
    public bool IsSeries { get; set; }
    
    public string? PreviewImageUrl { get; set; } = "";
    public string? ImageUrl { get; set; } = "";
    
    public int? KinopoiskId { get; set; }
    public int? TmdbId { get; set; }
    public string? ImdbId { get; set; }
    public string? WikidataId { get; set; }
    
    public DateTime? TmdbLastSynced { get; set; }
    public DateTime? WikidataLastSynced { get; set; }

    public List<FilmGuildDb> Guilds { get; set; } = [];
    
    public string SearchIndex { get; set; } = "";
}