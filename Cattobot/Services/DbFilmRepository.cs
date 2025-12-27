using Cattobot.Db;
using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Kinopoisk.Gateway;

namespace Cattobot.Services;

public class DbFilmRepository(
    IFilmsClient kinopoiskFilmsClient,
    CattobotDbContext dbDbContext
    ) : IFilmRepository
{
    public async Task<Guid> Add(FilmDb film, ulong addedBy, ulong guildId, CancellationToken ct = default)
    {
        film.AddedBy = addedBy;
        film.GuildId = guildId;
        film.AddedOn = DateTime.UtcNow;
        
        await dbDbContext.Films.AddAsync(film, ct);
        await dbDbContext.SaveChangesAsync(ct);
        return film.Id;
    }
    
    public async Task<IEnumerable<FilmSearchResponse_films>> GetSuggestionsFromKinopoisk(
        string query, int page, CancellationToken ct)
    {
        var result = await kinopoiskFilmsClient.SearchByKeywordAsync(query, page, ct);
        return result.Films;
    }
    
    public async Task<Film> GetFilmFromKinopoisk(int id, CancellationToken ct)
    {
        var result = await kinopoiskFilmsClient.FilmsAsync(id, ct);
        return result;
    }
}