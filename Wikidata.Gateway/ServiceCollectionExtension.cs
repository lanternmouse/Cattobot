using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wikidata.Gateway.Configuration;

namespace Wikidata.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddWikidataIntegration(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<WikidataOptions>(configuration.GetSection("Wikidata"));

        collection.AddScoped<IItemsClient>(s =>
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