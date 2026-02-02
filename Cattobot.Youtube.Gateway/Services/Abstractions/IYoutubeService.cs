using YoutubeDLSharp.Metadata;

namespace Cattobot.Youtube.Gateway.Services.Abstractions;

public interface IYoutubeService
{
    Task<string> GetAudioStreamUrl(string uri, CancellationToken ct);

    Task<IEnumerable<string>> GetYoutubeSearchSuggestions(string query);

    Task<string> GetYoutubeSearchResult(string query);

    Task<VideoData> GetYoutubeVideoInfo(string uri);
}