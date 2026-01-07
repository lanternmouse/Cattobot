using System.Diagnostics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YtDlp.Gateway.Configuration;
using YtDlp.Gateway.Models;
using YtDlp.Gateway.Services.Abstractions;

namespace YtDlp.Gateway.Services;

public class YtDlpService(
    IOptions<YtDlpOptions> options
    ) : IYtDlpService
{
    private const string YtSearchArguments = "--flat-playlist -j \"ytsearch{0}:{1}\"";
    private const string BestAudioUrlArguments = "-f bestaudio --get-url {0}";
    private const string MediaInfoArguments = "-j --print-json {0}";
    
    public async Task<string> GetAudioStreamUrl(string url)
    {
        using var result = GetProcessResult(string.Format(BestAudioUrlArguments, url));

        return await result.StandardOutput.ReadToEndAsync();
    }
    
    public async Task<IEnumerable<ShortMediaInfo>> GetYoutubeSearchResults(string query)
    {
        using var result = GetProcessResult(string.Format(YtSearchArguments, 10, query));

        List<ShortMediaInfo> videoInfos = [];
        
        while (true)
        {
            var videoOutput = await result.StandardOutput.ReadLineAsync();
            if (videoOutput is null) break;
            var info = JsonConvert.DeserializeObject<ShortMediaInfo>(videoOutput);
            if (info == null) continue;
            videoInfos.Add(info);
        }
        
        return videoInfos;
    }
    
    public async Task<FullMediaInfo> GetVideoInfo(string url)
    {
        using var result = GetProcessResult(string.Format(MediaInfoArguments, url));
        
        var videoOutput = await result.StandardOutput.ReadLineAsync();
        var info = JsonConvert.DeserializeObject<FullMediaInfo>(videoOutput!);

        return info!;
    }
    
    private Process GetProcessResult(string arguments)
    {
        arguments = "-q --no-warnings " + arguments;
        if (!string.IsNullOrEmpty(options.Value.CookiesFilePath))
            arguments = $"--cookies {options.Value.CookiesFilePath} " + arguments;
        
        return Process.Start(new ProcessStartInfo
        {
            FileName = options.Value.ExecutablePath,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
        }) ?? throw new InvalidOperationException("Could not start yt-dlp");
    }
}