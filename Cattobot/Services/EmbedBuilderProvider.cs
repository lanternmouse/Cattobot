using System.Globalization;
using Cattobot.Db.Models;
using Discord;

namespace Cattobot.Services;

public static class EmbedBuilderProvider
{
    public static EmbedBuilder GetShortFilmInfoEmbed(FilmDb filmDb)
    {
        return new EmbedBuilder
        {
            ThumbnailUrl = filmDb.PreviewImageUrl,
            Description = filmDb.Description,
            Fields =
            [
                new EmbedFieldBuilder()
                {
                    Name = "Жанр",
                    IsInline = true,
                    Value = string.Join(", ", filmDb.Genres)
                },
                new EmbedFieldBuilder
                {
                    Name = "Длительность",
                    IsInline = true,
                    Value = filmDb.Duration != null
                        ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration)).ToString("HH:mm")
                        : "Неизвестно"
                },
                new EmbedFieldBuilder()
                {
                    Name = "Рейтинг",
                    IsInline = true,
                    Value = filmDb.Rating.ToString(new CultureInfo("ru-RU")) ?? "-"
                }
            ]
        };
    }

    public static EmbedBuilder GetFullFilmInfoEmbed(FilmDb filmDb)
    {
        return new EmbedBuilder
        {
            ImageUrl = filmDb.ImageUrl,
            Description = filmDb.Description,
            Fields =
            [
                new EmbedFieldBuilder()
                {
                    Name = "Страна",
                    IsInline = true,
                    Value = string.Join(", ", filmDb.Countries)
                },
                new EmbedFieldBuilder()
                {
                    Name = "Жанр",
                    IsInline = true,
                    Value = string.Join(", ", filmDb.Genres)
                },
                new EmbedFieldBuilder
                {
                    Name = "Длительность",
                    IsInline = true,
                    Value = filmDb.Duration != null
                        ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration)).ToString("HH:mm")
                        : "Неизвестно"
                },
                new EmbedFieldBuilder()
                {
                    Name = "Рейтинг",
                    IsInline = true,
                    Value = filmDb.Rating.ToString(new CultureInfo("ru-RU")) ?? "-"
                }
            ]
        };
    }
}