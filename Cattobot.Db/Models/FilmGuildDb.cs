using Cattobot.Db.Models.Enums;

namespace Cattobot.Db.Models;

public record FilmGuildDb
{
    public Guid Id { get; set; }
    
    public Guid FilmId { get; set; }
    public FilmDb Film { get; set; } = null!;
    
    public ulong GuildId { get; set; }
    
    public FilmStatus FilmStatus { get; set; }
    public DateTime StatusOn { get; set; }

    public List<FilmGuildMemberDb> Members { get; set; } = [];
}