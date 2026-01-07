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
            Fields =
            [
                new EmbedFieldBuilder()
                {
                    Name = "Описание",
                    IsInline = false,
                    Value = filmDb.Description
                },
                new EmbedFieldBuilder()
                {
                    Name = "Страна",
                    IsInline = true,
                    Value = string.Join(", ", filmDb.Countries)
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
                },
                new EmbedFieldBuilder()
                {
                    Name = "Жанр",
                    IsInline = false,
                    Value = string.Join(", ", filmDb.Genres)
                },
            ]
        };
    }

    public static EmbedBuilder GetFullFilmInfoEmbed(FilmDb filmDb)
    {
        return new EmbedBuilder
        {
            ImageUrl = filmDb.ImageUrl,
            Fields =
            [
                new EmbedFieldBuilder()
                {
                    Name = "Описание",
                    IsInline = false,
                    Value = filmDb.Description
                },
                new EmbedFieldBuilder()
                {
                    Name = "Страна",
                    IsInline = true,
                    Value = string.Join(", ", filmDb.Countries)
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
                },
                new EmbedFieldBuilder()
                {
                    Name = "Жанр",
                    IsInline = false,
                    Value = string.Join(", ", filmDb.Genres)
                },
            ]
        };
    }
}