namespace Cattobot.Db.Models;

public class FilmGuildMemberDb
{
    public Guid Id { get; set; }
    public Guid FilmGuildId { get; set; }
    public ulong UserId { get; set; }
    public DateTime AddedOn { get; set; }
}