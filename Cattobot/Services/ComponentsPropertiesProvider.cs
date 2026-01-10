using Cattobot.Db.Models;
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
}