using Cattobot.Db.Models;
using Kinopoisk.Gateway;
using Mapster;

namespace Cattobot.Automapper;

public class KinopoiskProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Film, FilmDb>()
            .Map(d => d.KinopoiskId, s => s.KinopoiskId)
            .Map(d => d.Title, s => s.NameEn)
            .Map(d => d.LocalizedTitle, s => s.NameRu)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Duration, s => s.FilmLength ?? 0)
            .Map(d => d.Year, s => s.Year)
            .Map(d => d.RatingImdb, s => s.RatingImdb);
    }
}