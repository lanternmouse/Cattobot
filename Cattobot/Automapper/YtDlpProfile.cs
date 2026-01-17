using System.Globalization;
using Cattobot.Db.Models;
using Cattobot.Youtube.Gateway.Models;
using Mapster;
using Nager.Country.Translation;
using TMDbLib.Objects.Movies;

namespace Cattobot.Automapper;

public class YtDlpProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FullMediaInfo, TrackDb>()
            .Map(d => d.Id, s => Guid.Empty)
            .Map(d => d.Title, s => s.Title)
            .Map(d => d.AddedOn, s => DateTime.UtcNow)
            .Map(d => d.Artist, s => s.Channel)
            .Map(d => d.Duration, s => s.Duration)
            .Map(d => d.ThumbnailUrl, s => s.Thumbnail)
            .Map(d => d.ExternalUrl, s => s.WebpageUrl);

        config.NewConfig<YoutubeVideoInfo.Root, TrackDb>()
            .Map(d => d.Id, s => Guid.Empty)
            .Map(d => d.Title, s => s.VideoDetails.Title)
            .Map(d => d.Artist, s => s.VideoDetails.Author)
            .Map(d => d.ArtistUrl, s => "https://www.youtube.com/channel/" + s.VideoDetails.ChannelId)
            .Map(d => d.Duration, s => int.Parse(s.VideoDetails.LengthSeconds))
            .Map(d => d.ExternalUrl, s => "https://www.youtube.com/watch?v=" + s.VideoDetails.VideoId)
            .Map(d => d.ThumbnailUrl, s => s.VideoDetails.Thumbnail.Thumbnails.Last().Url);
    }
}