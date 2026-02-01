using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using NetCord;
using NetCord.Rest;

namespace Cattobot.Services;

public static class ComponentsPropertiesProvider
{
    public static ActionRowProperties PickedFilmInfoComponents(FilmDb filmDb, string watchUrlTemplate)
    {
        var props = new ActionRowProperties();

        if (filmDb.KinopoiskId.HasValue)
        {
            var url = watchUrlTemplate.Replace($"{{{nameof(FilmDb.KinopoiskId)}}}", filmDb.KinopoiskId.ToString());
            props.Add(new LinkButtonProperties(url, "Смотреть"));
        }

        props.AddComponents(
            new ButtonProperties($"filmMarkAsWatched:{filmDb.Id}", "В просмотренные", ButtonStyle.Primary),
            new ButtonProperties($"filmMarkAsAbandoned:{filmDb.Id}", "В брошенные", ButtonStyle.Secondary));

        return props;
    }
    
    public static ActionRowProperties MusicPlayerComponents(MusicPlayerStatus status)
    {
        var props = new ActionRowProperties();

        var resumePauseButton = status == MusicPlayerStatus.Paused
            ? new ButtonProperties("musicResume", "Играть", ButtonStyle.Primary)
            : new ButtonProperties("musicPause", "Пауза", ButtonStyle.Secondary);

        props.AddComponents(
            resumePauseButton,
            new ButtonProperties("musicSkipBackward", "Назад", ButtonStyle.Primary),
            new ButtonProperties("musicSkipForward", "Вперёд", ButtonStyle.Primary)
            // new ButtonProperties("musicShuffle", "Вперемешку", ButtonStyle.Secondary),
            // new ButtonProperties("musicShuffle", "Повтор", ButtonStyle.Secondary),
            // new ButtonProperties("musicList", "Список очереди", ButtonStyle.Secondary)
        );

        return props;
    }

    public static ActionRowProperties AddedPlaylistItemComponents(TrackQueueItemDb itemDb)
    {
        var props = new ActionRowProperties();

        props.AddComponents(
            new ButtonProperties($"musicSkipTo:{itemDb.Id}:{itemDb.TrackId}", "Играть сейчас", ButtonStyle.Secondary));
            // new ButtonProperties($"musicAdd:{itemDb.TrackId}", "Добавить в очередь", ButtonStyle.Secondary));

        return props;
    }

    public static ActionRowProperties AddedTrackItemComponents(TrackQueueItemDb itemDb)
    {
        var props = new ActionRowProperties();

        props.AddComponents(
            new ButtonProperties($"musicSkipTo:{itemDb.Id}:{itemDb.TrackId}", "Играть сейчас", ButtonStyle.Secondary),
            new ButtonProperties($"musicAdd:{itemDb.TrackId}", "Добавить в очередь", ButtonStyle.Secondary));

        return props;
    }
}