using System.Globalization;
using Cattobot.Db.Models;
using NetCord.Rest;

namespace Cattobot.Services;

public static class EmbedPropertiesProvider
{
    public static EmbedProperties GetShortFilmInfoEmbed(FilmDb filmDb)
    {
        var fields = new List<EmbedFieldProperties>();
        if (!string.IsNullOrEmpty(filmDb.Description))
            fields.Add(new EmbedFieldProperties
            {
                Name = "Описание",
                Inline = false,
                Value = filmDb.Description
            });

        fields.AddRange([
            new EmbedFieldProperties
            {
                Name = "Жанр",
                Inline = false,
                Value = string.Join(", ", filmDb.Genres)
            },
            new EmbedFieldProperties
            {
                Name = "Страна",
                Inline = true,
                Value = string.Join(", ", filmDb.Countries)
            },
            new EmbedFieldProperties
            {
                Name = "Длительность",
                Inline = true,
                Value = filmDb.Duration != null
                    ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration.Value)).ToString("HH:mm")
                    : "Неизвестно"
            },
            new EmbedFieldProperties
            {
                Name = "Рейтинг",
                Inline = true,
                Value = filmDb.Rating.ToString(new CultureInfo("ru-RU")) ?? "-"
            }
        ]);

        return new EmbedProperties
        {
            Thumbnail = new EmbedThumbnailProperties(filmDb.PreviewImageUrl),
            Fields = fields
        };
    }

    public static EmbedProperties GetFullFilmInfoEmbed(FilmDb filmDb)
    {
        var fields = new List<EmbedFieldProperties>();
        if (!string.IsNullOrEmpty(filmDb.Description))
            fields.Add(new EmbedFieldProperties
            {
                Name = "Описание",
                Inline = false,
                Value = filmDb.Description
            });

        fields.AddRange([
            new EmbedFieldProperties
            {
                Name = "Жанр",
                Inline = false,
                Value = string.Join(", ", filmDb.Genres)
            },
            new EmbedFieldProperties
            {
                Name = "Страна",
                Inline = true,
                Value = string.Join(", ", filmDb.Countries)
            },
            new EmbedFieldProperties
            {
                Name = "Длительность",
                Inline = true,
                Value = filmDb.Duration != null
                    ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration.Value)).ToString("HH:mm")
                    : "Неизвестно"
            },
            new EmbedFieldProperties
            {
                Name = "Рейтинг",
                Inline = true,
                Value = filmDb.Rating.ToString(new CultureInfo("ru-RU")) ?? "-"
            }
        ]);

        return new EmbedProperties
        {
            Image = new EmbedImageProperties(filmDb.ImageUrl),
            Fields = fields
        };
    }
}