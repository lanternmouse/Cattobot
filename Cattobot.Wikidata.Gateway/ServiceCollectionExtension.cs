using Cattobot.Wikidata.Gateway.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cattobot.Wikidata.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddWikidataIntegration(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<WikidataOptions>(configuration.GetSection("Wikidata"));

        collection.AddSingleton<IItemsClient>(s =>
        {
            var options = s.GetRequiredService<IOptions<WikidataOptions>>();
            return new ItemsClient(
                options.Value.Url,
                new HttpClient
                {
                    BaseAddress = new Uri(options.Value.Url)
                }, 
                options)
            {
                BaseUrl = options.Value.Url
            };
        });
    }
}