using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tmdb.Gateway.Configuration;

namespace Tmdb.Gateway;

public partial class MoviesClient
{
    private readonly IOptions<TmdbOptions> _options;
    
    public MoviesClient(string baseUrl, HttpClient httpClient, IOptions<TmdbOptions> options)
    {
        _baseUrl = baseUrl;
        _httpClient = httpClient;
        _settings = new Lazy<JsonSerializerSettings>(new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        });
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
        
        request.Headers.Add("Authorization", "Bearer " + _options.Value.Token);
    }
}

public partial class SearchClient
{
    private readonly IOptions<TmdbOptions> _options;
    
    public SearchClient(string baseUrl, HttpClient httpClient, IOptions<TmdbOptions> options)
    {
        _baseUrl = baseUrl;
        _httpClient = httpClient;
        _settings = new Lazy<JsonSerializerSettings>(new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        });
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
        
        request.Headers.Add("Authorization", "Bearer " + _options.Value.Token);
    }
}