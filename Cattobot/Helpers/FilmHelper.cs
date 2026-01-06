using Cattobot.Db.Models;

namespace Cattobot.Helpers;

public static class FilmHelper
{
    public static string BuildTitleWithMarkdownUrl(FilmDb filmDb)
    {
        return $"[{filmDb.LocalizedTitle} ({filmDb.Year})](<https://www.kinopoisk.ru/film/{filmDb.KinopoiskId}>)";
    }
}