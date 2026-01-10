using System.Globalization;
using Cattobot.Db.Models;
using Mapster;
using Nager.Country.Translation;
using TMDbLib.Objects.Movies;

namespace Cattobot.Automapper;

public class TmdbProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Movie, FilmDb>()
            .Map(d => d.Id, s => Guid.Empty)
            .Map(d => d.TmdbId, s => s.Id)
            .Map(d => d.ImdbId, s => s.ImdbId)
            .Map(d => d.Title, s => s.Title)
            .Map(d => d.Year, s => s.ReleaseDate.HasValue ? s.ReleaseDate.Value.Year : (int?)null)
            .Map(d => d.SearchIndex,
                s => s.ReleaseDate.HasValue
                    ? $"{s.OriginalTitle} {s.Title} {s.ReleaseDate.Value.Year}"
                    : $"{s.OriginalTitle} {s.Title}")
            .Map(d => d.Description, s => s.Overview)
            .Map(d => d.Duration, s => s.Runtime)
            .Map(d => d.Actors, s => s.Credits.Cast.Select(x => x.Name))
            .Map(d => d.Directors, s => s.Credits.Crew.Where(x => x.Job == "Director").Select(x => x.Name))
            .Map(d => d.ReleaseDate,
                s => s.ReleaseDate.HasValue ? DateOnly.FromDateTime(s.ReleaseDate.Value) : (DateOnly?)null)
            .Map(d => d.Rating, s => s.VoteAverage)
            .Map(d => d.Countries, s => s.ProductionCountries.Select(x => GetLocalizedCountry(x.Iso_3166_1)))
            .Map(d => d.Genres, s => s.Genres.Select(x => x.Name))
            .Map(d => d.PreviewImageUrl, s => CreateImgLink(s.PosterPath ?? s.BackdropPath))
            .Map(d => d.ImageUrl, s => CreateImgLink(s.BackdropPath ?? s.PosterPath))
            .Map(d => d.WikidataId, s => s.ExternalIds.WikidataId)
            .Map(d => d.IsSeries, s => false);
    }

    private static string? CreateImgLink(string? imgPath)
    {
        return string.IsNullOrEmpty(imgPath) ? null : "https://image.tmdb.org/t/p/w1280" + imgPath;
    }

    private static string? GetLocalizedCountry(string isoCode)
    {
        if (isoCode.Equals("su", StringComparison.OrdinalIgnoreCase)) return "СССР";
        
        return new TranslationProvider().GetCountryTranslatedName(isoCode, new CultureInfo("ru-RU"));
    }
}