using Cattobot.Db.Models;
using Kinopoisk.Gateway;

namespace Cattobot.Services.Abstractions;

public interface IFilmRepository
{
    Task<Guid> Add(FilmDb film, ulong addedBy, ulong guildId, CancellationToken ct = default);
    
    Task<IEnumerable<FilmSearchResponse_films>> GetSuggestionsFromKinopoisk(string query, int page, 
        CancellationToken ct = default);

    Task<Film> GetFilmFromKinopoisk(int id, CancellationToken ct = default);
}