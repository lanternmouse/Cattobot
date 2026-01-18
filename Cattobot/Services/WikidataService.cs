using Cattobot.Db.Models;
using Cattobot.Services.Abstractions;
using Cattobot.Wikidata.Gateway;
using Cattobot.Wikidata.Gateway.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Cattobot.Services;

public class WikidataService(
    IItemsClient wikidataItemsClient,
    IOptions<WikidataOptions> options)
    : IWikidataService
{
    public async Task<int?> GetKinopoiskId(string wikidataId, CancellationToken stoppingToken = default)
    {
        var data = await wikidataItemsClient.GetItemAsync(wikidataId, [Anonymous.Statements], [], "",
            [],
            "",
            options.Value.Token,
            stoppingToken);
        
        if (data.Statements.AdditionalProperties.TryGetValue("P2603", out var kinopoiskJson))
        {
            var id = (kinopoiskJson as JArray)?.First?["value"]?["content"]?.Value<int>();

            return id;
        }

        return null;
    }
}