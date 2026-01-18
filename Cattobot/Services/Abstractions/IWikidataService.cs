namespace Cattobot.Services.Abstractions;

public interface IWikidataService
{
    Task<int?> GetKinopoiskId(string wikidataId, CancellationToken stoppingToken = default);
}