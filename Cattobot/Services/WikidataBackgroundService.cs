using Cattobot.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Wikidata.Gateway;
using Wikidata.Gateway.Configuration;

namespace Cattobot.Services;

public class WikidataBackgroundService(
    IItemsClient wikidataItemsClient,
    IOptions<WikidataOptions> options,
    CattobotDbContext dbContext,
    ILogger<WikidataBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var unsyncedWikidata = await dbContext.Films
                .Where(x => x.WikidataLastSynced == null)
                .Select(x => new { x.Id, x.WikidataId })
                .ToListAsync(stoppingToken);

            foreach (var film in unsyncedWikidata)
            {
                try
                {
                    var data = await wikidataItemsClient.GetItemAsync(film.WikidataId, [Anonymous.Statements], [], "",
                        [],
                        "",
                        options.Value.Token,
                        stoppingToken);

                    int? kinopoiskId = null;
                    if (data.Statements.AdditionalProperties.TryGetValue("P2603", out var kinopoiskJson))
                    {
                        var id = (kinopoiskJson as JArray)?.First?["value"]?["content"]?.Value<int>();
                        if (id != null)
                            kinopoiskId = id;
                    }

                    await dbContext.Films.Where(x => x.Id == film.Id)
                        .ExecuteUpdateAsync(x => x
                                .SetProperty(p => p.KinopoiskId, kinopoiskId)
                                .SetProperty(p => p.WikidataLastSynced, DateTime.UtcNow),
                            stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured while retrieving data: {ExceptionMessage}", ex.Message);
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }

    private record Statement
    {
        
    }
}