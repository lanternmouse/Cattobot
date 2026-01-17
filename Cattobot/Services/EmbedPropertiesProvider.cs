using System.Globalization;
using Cattobot.Db.Models;
using Cattobot.Extensions;
using Cattobot.Helpers;
using NetCord.Rest;

namespace Cattobot.Services;

public static class EmbedPropertiesProvider
{
    # region Films
    
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
                    ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration.Value)).ToNiceDuration()
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
                    ? TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(filmDb.Duration.Value)).ToNiceDuration()
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
    
    # endregion
    
    # region Music
    
    public static EmbedProperties GetTrackItemEmbed(TrackQueueItemDb item)
    {
        var trackDb = item.Track;
        
        var fields = new List<EmbedFieldProperties>();
        
        var props = new EmbedProperties()
        {
            Title = trackDb.Title,
            Url = trackDb.ExternalUrl,
            Fields = fields
        };

        if (!string.IsNullOrEmpty(trackDb.ThumbnailUrl))
            props.Thumbnail = new EmbedThumbnailProperties(trackDb.ThumbnailUrl);

        props.Author = new EmbedAuthorProperties
        {
            Url = trackDb.ArtistUrl,
            Name = trackDb.Artist,
        };
        
        fields.Add(new EmbedFieldProperties
        {
            Name = "Добавил",
            Inline = true,
            Value = $"<@{item.UserId}>"
        });
        
        fields.Add(new EmbedFieldProperties
        {
            Name = "Длительность",
            Inline = true,
            Value = TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(trackDb.Duration)).ToNiceDuration()
        });

        return props;
    }

    # endregion
}
