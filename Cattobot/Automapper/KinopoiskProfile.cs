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
            .Map(d => d.Title, s => s.NameEn ?? "")
            .Map(d => d.LocalizedTitle, s => s.NameRu)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Duration, s => s.FilmLength ?? 0)
            .Map(d => d.Year, s => s.Year)
            .Map(d => d.Rating, s => s.RatingImdb)
            .Map(d => d.Countries, s => s.Countries.Select(c => c.Country1))
            .Map(d => d.Genres, s => s.Genres.Select(c => c.Genre1))
            .Map(d => d.ImageUrl, s => s.CoverUrl ?? s.PosterUrl)
            .Map(d => d.PreviewImageUrl, s => s.PosterUrlPreview)
            .Map(d => d.IsSeries, s => s.Serial ?? false);

        config.NewConfig<FilmSearchResponse_films, FilmDb>()
            .Map(d => d.KinopoiskId, s => s.FilmId)
            .Map(d => d.Title, s => s.NameEn ?? "")
            .Map(d => d.LocalizedTitle, s => s.NameRu)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Duration, s => TimeOnly.ParseExact(s.FilmLength, "HH:mm").ToTimeSpan().TotalMinutes)
            .Map(d => d.Year, s => s.Year)
            .Map(d => d.Rating, s => s.Rating)
            .Map(d => d.Countries, s => s.Countries.Select(c => c.Country1))
            .Map(d => d.Genres, s => s.Genres.Select(c => c.Genre1))
            .Map(d => d.ImageUrl, s => s.PosterUrl)
            .Map(d => d.PreviewImageUrl, s => s.PosterUrlPreview)
            .Map(d => d.IsSeries, s => s.Type == FilmSearchResponse_filmsType.MINI_SERIES ||
                                       s.Type == FilmSearchResponse_filmsType.TV_SERIES || 
                                       s.Type == FilmSearchResponse_filmsType.TV_SHOW);
    }
}