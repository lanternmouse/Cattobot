using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;

namespace Cattobot.Services.Abstractions;

public interface IFilmRepository
{
    Task<Guid> Add(FilmDb film, ulong userId, ulong guildId, CancellationToken ct = default);

    IQueryable<FilmGuildDb> GetGuildListQuery(ulong guildId, ulong? userId, FilmStatus[] statuses);

    Task<FilmDb> Get(Guid id, CancellationToken ct = default);
    
    Task RemoveGuildMember(Guid id, ulong userId, ulong guildId, CancellationToken ct = default);

    Task SetGuildStatus(Guid id, ulong guildId, FilmStatus status, CancellationToken ct = default);
}