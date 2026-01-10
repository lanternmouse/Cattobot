using Cattobot.Db.Models;

namespace Cattobot.Helpers;

public static class FilmHelper
{
    public static string BuildTitleWithMarkdownUrl(FilmDb filmDb)
    {
        return filmDb.Year.HasValue
            ? $"[{filmDb.Title} ({filmDb.Year})](<https://www.themoviedb.org/movie/{filmDb.TmdbId}>)"
            : $"[{filmDb.Title}](<https://www.themoviedb.org/movie/{filmDb.TmdbId}>)";
    }
}