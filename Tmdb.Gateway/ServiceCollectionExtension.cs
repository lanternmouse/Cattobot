using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tmdb.Gateway.Configuration;

namespace Tmdb.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddTmdbIntegration(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<TmdbOptions>(configuration.GetSection("Tmdb"));

        collection.AddScoped<ISearchClient>(s =>
        {
            var options = s.GetRequiredService<IOptions<TmdbOptions>>();
            return new SearchClient(
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
        
        collection.AddScoped<IMoviesClient>(s =>
        {
            var options = s.GetRequiredService<IOptions<TmdbOptions>>();
            return new MoviesClient(
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