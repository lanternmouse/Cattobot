using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Cattobot.Youtube.Gateway.Models;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using MapsterMapper;

namespace Cattobot.Services;

public class TrackQueueService(
    ITrackQueueRepository queueRepo,
    ITrackRepository trackRepo,
    IYoutubeService youtubeService,
    IMapper mapper
    ) : ITrackQueueService
{
    public async Task<TrackQueueItemDb> EnqueueFromQuery(ulong guildId, ulong userId, string query, CancellationToken ct = default)
    {
        var queue = await queueRepo.GetOrCreate(guildId, ct);

        YoutubeVideoInfo.Root mediaInfo;
        if (Uri.IsWellFormedUriString(query, UriKind.Absolute))
        {
            mediaInfo = await youtubeService.GetYoutubeVideoInfo(query);
        }
        else
        {
            var videoUrl = await youtubeService.GetYoutubeSearchResult(query);
            mediaInfo = await youtubeService.GetYoutubeVideoInfo(videoUrl);
        }

        var trackDb = mapper.Map<TrackDb>(mediaInfo);
        
        var trackId = await trackRepo.Add(trackDb, ct);

        var itemId = await queueRepo.Append(queue.Id, trackId, userId, ct);
        
        var trackItem = await queueRepo.GetItem(itemId, ct);

        return trackItem!;
    }
}