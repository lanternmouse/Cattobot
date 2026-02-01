using Cattobot.Db.Models;
using Cattobot.Youtube.Gateway.Models;
using Mapster;
using YoutubeDLSharp.Metadata;

namespace Cattobot.Automapper;

public class YoutubeProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<YoutubeVideoInfo.Root, TrackDb>()
            .Map(d => d.Id, s => Guid.Empty)
            .Map(d => d.Title, s => s.VideoDetails.Title)
            .Map(d => d.Artist, s => s.VideoDetails.Author)
            .Map(d => d.ArtistUrl, s => "https://www.youtube.com/channel/" + s.VideoDetails.ChannelId)
            .Map(d => d.Duration, s => int.Parse(s.VideoDetails.LengthSeconds))
            .Map(d => d.ExternalUrl, s => "https://www.youtube.com/watch?v=" + s.VideoDetails.VideoId)
            .Map(d => d.ThumbnailUrl, s => s.VideoDetails.Thumbnail.Thumbnails.Last().Url);

        config.NewConfig<VideoData, TrackDb>()
            .Map(d => d.Id, s => Guid.Empty)
            .Map(d => d.Title, s => s.Title)
            .Map(d => d.Artist, s => s.Channel)
            .Map(d => d.ArtistUrl, s => s.ChannelUrl)
            .Map(d => d.Duration, s => (int)(s.Duration ?? 0))
            .Map(d => d.ExternalUrl, s => s.WebpageUrl ?? s.Url)
            .Map(d => d.ThumbnailUrl, s => s.Thumbnails.Last().Url);
    }
}