using Cattobot.Youtube.Gateway.Models;

namespace Cattobot.Youtube.Gateway.Services.Abstractions;

public interface IYoutubeService
{
    Task<string> GetAudioStreamUrl(string uri);

    Task<IEnumerable<string>> GetYoutubeSearchSuggestions(string query);

    Task<string> GetYoutubeSearchResult(string query);

    Task<YoutubeVideoInfo.Root> GetYoutubeVideoInfo(string uri);
}