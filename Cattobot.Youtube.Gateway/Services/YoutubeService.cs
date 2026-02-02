using System.Text.RegularExpressions;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using YoutubeDLSharp;
using Microsoft.Extensions.Caching.Memory;
using System.Web;
using YoutubeDLSharp.Metadata;
using YoutubeDLSharp.Options;

namespace Cattobot.Youtube.Gateway.Services;

public partial class YoutubeService(
    IMemoryCache cache
    ) : IYoutubeService
{
    private readonly YoutubeDL _ytdl = new();

    private readonly HttpClient _httpClient = new()
    {
        DefaultRequestHeaders =
            { { "User-Agent", "com.google.android.youtube/20.01.58 (Linux; U; Android 12; GB) gzip" } }
    };

    private const string YoutubeSearchUrl =
        "https://suggestqueries-clients6.youtube.com/complete/search?ds=yt&hl=en&gl=nl&client=youtube&gs_ri=youtube&tok=&h=180&w=320&ytvs=1&gs_id=5&q={0}&cp={1}";

    private const string YoutubeSearchRequestUrl = "https://www.youtube.com/youtubei/v1/search?key={0}";

    private const string YoutubeSearchRequestBody =
        // lang=json
        """{"query": "{{0}}", "context": {"client": {"clientName": "ANDROID", "clientVersion": "20.10.38", "androidSdkVersion": 35, "userInterfaceTheme": "USER_INTERFACE_THEME_DARK", "hl": "en", "gl": "US", "deviceMake": "Google", "deviceModel": "Pixel 9 Pro"}}}""";

    public async Task<string> GetAudioStreamUrl(string url, CancellationToken ct)
    {
        var cacheKey = $"ytdl:{url}";

        if (cache.TryGetValue<string>(cacheKey, out var streamUrl))
        {
            var uri = new Uri(streamUrl!);
            var query = HttpUtility.ParseQueryString(uri.Query);
            var expireString = query["expire"];

            if (long.TryParse(expireString, out var expireUnixSeconds))
            {
                var expireDate = DateTimeOffset.FromUnixTimeSeconds(expireUnixSeconds).UtcDateTime;
                var now = DateTime.UtcNow;
                if (now < expireDate)
                {
                    return streamUrl!;
                }
            }
        }

        var data = await _ytdl.RunVideoDataFetch(url, ct);

        var itags = new[] { "774", "251", "141", "250", "140" };

        foreach (var itag in itags)
        {
            var format = data.Data.Formats.FirstOrDefault(x => x.FormatId == itag);
            if (format != null)
            {
                streamUrl = format.Url;
                cache.Set<string>(cacheKey, streamUrl);
                return streamUrl;
            }
        }

        return "";
    }

    public async Task<IEnumerable<string>> GetYoutubeSearchSuggestions(string query)
    {
        var response = await _httpClient.GetAsync(string.Format(YoutubeSearchUrl, Uri.EscapeDataString(query), query.Length));

        response.EnsureSuccessStatusCode();

        var responseMessage = await response.Content.ReadAsStringAsync();

        return SearchSuggestionRegex().Matches(responseMessage)
            .Select(match => Regex.Unescape(match.Groups[1].Value));
    }

    public async Task<VideoData> GetYoutubeVideoInfo(string uri)
    {
        var data = await _ytdl.RunVideoDataFetch(uri, overrideOptions: new OptionSet()
        {
            ExtractorArgs = new MultiValue<string>("youtube:player_skip=webpage,configs,js", "youtubetab:skip=webpage"),
            CheckFormats = false,
            CleanInfoJson = false
        });

        if (data.ErrorOutput.Length > 0)
            throw new InvalidOperationException(string.Join(", ", data.ErrorOutput));

        return data.Data;
    }

    public async Task<string> GetYoutubeSearchResult(string query)
    {
        var requestBody = YoutubeSearchRequestBody.Replace("{{0}}", query).Replace("{{1}}", "");
        var url = string.Format(YoutubeSearchRequestUrl, "AIzaSyA8eiZmM1FaDVjRy-df2KTyQ_vz_yYM39w");

        var response = await _httpClient.PostAsync(url, new StringContent(requestBody));

        response.EnsureSuccessStatusCode();

        var responseMessage = await response.Content.ReadAsStringAsync();

        var matches = SearchSuggestionsJsonRegex().Matches(responseMessage);

        return matches[0].Groups[2].Success
            ? "https://www.youtube.com/watch?v=" + matches[0].Groups[2].Value
            : "https://www.youtube.com/playlist?list=" + matches[0].Groups[1].Value;
    }

    [GeneratedRegex(@"\[""([^""]+)""")]
    private static partial Regex SearchSuggestionRegex();
    
    [GeneratedRegex(@"""playlistId"": ""([OL|PL][A-Za-z0-9_-]+)""|""videoId"": ""([A-Za-z0-9_-]+)""")]
    private static partial Regex SearchSuggestionsJsonRegex();
}
