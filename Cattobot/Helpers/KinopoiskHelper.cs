namespace Cattobot.Helpers;

public static class KinopoiskHelper
{
    public static string BuildUrl(int kinopoiskId)
    {
        return $"https://www.kinopoisk.ru/film/{kinopoiskId}";
    }
}