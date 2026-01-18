using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Cattobot.Youtube.Gateway.Configuration;
using Cattobot.Youtube.Gateway.Models;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Cattobot.Youtube.Gateway.Services;

public partial class YoutubeService(
    IOptions<YtDlpOptions> options
    ) : IYoutubeService
{
    private string? _visitorData;

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
        """{"query": "{{0}}", "params": "{{1}}", "context": {"client": {"clientName": "ANDROID", "clientVersion": "20.10.38", "androidSdkVersion": 35, "userInterfaceTheme": "USER_INTERFACE_THEME_DARK", "hl": "en", "gl": "US", "deviceMake": "Google", "deviceModel": "Pixel 9 Pro"}}}""";
    
    private const string YoutubeVideoRequestUrl = "https://www.youtube.com/youtubei/v1/player?key={0}";

    private const string YoutubeVideoRequestBody =
        // lang=json
        """{"videoId": "{0}", "contentCheckOk": true, "context": {"client": {"clientName": "ANDROID", "clientVersion": "20.10.38", "visitorData": "{1}", "osVersion": "12", "hl": "en", "gl": "US", "platform": "MOBILE", "osName": "Android"}}}""";
    
    public async Task<string> GetAudioStreamUrl(string uri)
    {
        var info = await GetYoutubeVideoInfo(uri);
        
        var itags = new[] { 774, 251, 141, 250, 140 };
        
        foreach (var itag in itags)
        {
            var format = info.StreamingData.AdaptiveFormats.FirstOrDefault(x => x.Itag == itag);
            if (format != null)
                return format.Url;
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
    
    public async Task<YoutubeVideoInfo.Root> GetYoutubeVideoInfo(string uri)
    {
        await GetVisitorData();
        
        var videoId = uri.Split("v=")[1];

        var requestBody = YoutubeVideoRequestBody.Replace("{0}", videoId).Replace("{1}", _visitorData);
        var url = string.Format(YoutubeVideoRequestUrl, "AIzaSyA8eiZmM1FaDVjRy-df2KTyQ_vz_yYM39w");

        var response = await _httpClient.PostAsync(url, new StringContent(requestBody));
        
        response.EnsureSuccessStatusCode();

        var responseMessage = await response.Content.ReadAsStringAsync();

        var info = JsonConvert.DeserializeObject<YoutubeVideoInfo.Root>(responseMessage);

        return info!;
    }

    public async Task<string> GetYoutubeSearchResult(string query)
    {
        var requestBody = YoutubeSearchRequestBody.Replace("{{0}}", query).Replace("{{1}}", "EgIQAQ==");
        var url = string.Format(YoutubeSearchRequestUrl, "AIzaSyA8eiZmM1FaDVjRy-df2KTyQ_vz_yYM39w");

        var response = await _httpClient.PostAsync(url, new StringContent(requestBody));
        
        response.EnsureSuccessStatusCode();

        var responseMessage = await response.Content.ReadAsStringAsync();

        var info = JsonConvert.DeserializeObject<YoutubeSearchResults>(responseMessage);

        var videoId = info?.Contents.SectionListRenderer.Contents
            .FirstOrDefault(c => c.ItemSectionRenderer.Contents.Any(x => x.CompactVideoRenderer != null))?
            .ItemSectionRenderer.Contents
            .FirstOrDefault(x => x.CompactVideoRenderer != null)?.CompactVideoRenderer?.VideoId;

        return "https://www.youtube.com/watch?v=" + videoId;
    }

    private async Task<string> GetVisitorData()
    {
        if (!string.IsNullOrWhiteSpace(_visitorData))
            return _visitorData;

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "https://www.youtube.com/sw.js_data"
        );

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Headers.Add(
            "User-Agent",
            "com.google.android.youtube/20.10.38 (Linux; U; ANDROID 11) gzip"
        );

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        if (jsonString.StartsWith(")]}'"))
            jsonString = jsonString[4..];

        var json = JsonDocument.Parse(jsonString);

        var value = json.RootElement[0][2][0][0][13].GetString();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException();
        }

        return _visitorData = value;
    }

    [GeneratedRegex(@"\[""([^""]+)""")]
    private static partial Regex SearchSuggestionRegex();
}