using YtDlp.Gateway.Models;

namespace YtDlp.Gateway.Services.Abstractions;

public interface IYtDlpService
{
    Task<string> GetAudioStreamUrl(string url);

    Task<IEnumerable<ShortMediaInfo>> GetYoutubeSearchResults(string query);

    Task<FullMediaInfo> GetVideoInfo(string url);
}