using Cattobot.Db;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cattobot.Services;

public class WikidataBackgroundService(
    IServiceScopeFactory scopeFactory,
    IWikidataService wikidataService,
    ILogger<WikidataBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(30 * 60 * 1000, stoppingToken);
            
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CattobotDbContext>();
            
            var unsyncedWikidata = await dbContext.Films
                .Where(x => x.WikidataLastSynced == null && x.WikidataId != null)
                .Select(x => new { x.Id, x.WikidataId })
                .ToListAsync(stoppingToken);

            foreach (var film in unsyncedWikidata)
            {
                try
                {
                    var kinopoiskId = await wikidataService.GetKinopoiskId(film.WikidataId!, stoppingToken);

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
        }
    }
}