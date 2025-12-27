using System.Net.Http.Headers;
using Kinopoisk.Gateway.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kinopoisk.Gateway;

public partial class FilmsClient
{
    private readonly IOptions<KinopoiskOptions> _options;
    
    public FilmsClient(string baseUrl, HttpClient httpClient, IOptions<KinopoiskOptions> options)
    {
        _baseUrl = baseUrl;
        _httpClient = httpClient;
        _settings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings);
        _options = options;
    }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        if (request.Content is StringContent content)
        {
            var json = content.ReadAsStringAsync().Result;
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        }

        request.Headers.Add("X-API-KEY", _options.Value.Token);
    }
}