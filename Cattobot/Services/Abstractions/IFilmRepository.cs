using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface IFilmRepository
{
    Task<Guid> Add(FilmDb film, ulong addedBy, ulong guildId, CancellationToken ct = default);

    IQueryable<FilmDb> GetListQuery(ulong guildId, ulong? userId);

    Task<FilmDb> Get(Guid id, CancellationToken ct = default);
    
    Task Remove(Guid id, CancellationToken ct = default);

    Task MarkWatched(Guid id, CancellationToken ct = default);
}