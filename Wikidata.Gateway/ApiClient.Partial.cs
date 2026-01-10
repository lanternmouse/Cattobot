using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wikidata.Gateway.Configuration;

namespace Wikidata.Gateway;

public partial class ItemsClient
{
    private readonly IOptions<WikidataOptions> _options;
    
    public ItemsClient(string baseUrl, HttpClient httpClient, IOptions<WikidataOptions> options)
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

        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Cattobot", "1.0"));
        // request.Headers.Add("Authorization", "Bearer " + _options.Value.Token);
    }
}