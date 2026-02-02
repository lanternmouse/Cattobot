using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using MapsterMapper;
using YoutubeDLSharp.Metadata;

namespace Cattobot.Services;

public class TrackQueueService(
    ITrackQueueRepository queueRepo,
    ITrackRepository trackRepo,
    IYoutubeService youtubeService,
    IMapper mapper
    ) : ITrackQueueService
{
    public async Task<(TrackQueueItemDb item, VideoData videoData)> EnqueueFromQuery(ulong guildId, ulong userId, string query, 
        CancellationToken ct = default)
    {
        var queue = await queueRepo.GetOrCreate(guildId, ct);

        VideoData mediaInfo;
        
        if (Uri.IsWellFormedUriString(query, UriKind.Absolute))
        {
            mediaInfo = await youtubeService.GetYoutubeVideoInfo(query);
        }
        else
        {
            var videoUrl = await youtubeService.GetYoutubeSearchResult(query);
            mediaInfo = await youtubeService.GetYoutubeVideoInfo(videoUrl);
        }

        Guid itemId;
        
        if (mediaInfo.ResultType == MetadataType.Playlist)
        {
            var tracks = mapper.Map<List<TrackDb>>(mediaInfo.Entries);
            var trackIds = await trackRepo.AddRange(tracks, ct);
            itemId = await queueRepo.AppendRange(queue.Id, trackIds, userId, ct);
        }
        else
        {
            var trackDb = mapper.Map<TrackDb>(mediaInfo);
            var trackId = await trackRepo.Add(trackDb, ct);
            itemId = await queueRepo.Append(queue.Id, trackId, userId, ct);
        }
        
        var trackItem = await queueRepo.GetItem(itemId, ct);
        
        // preload source
        _ = Task.Run(async () => await youtubeService.GetAudioStreamUrl(trackItem!.Track.ExternalUrl, ct), ct);

        return (trackItem!, mediaInfo);
    }
}