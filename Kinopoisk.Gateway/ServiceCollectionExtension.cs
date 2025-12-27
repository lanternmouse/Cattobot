using Kinopoisk.Gateway.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kinopoisk.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddKinopoiskIntegration(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<KinopoiskOptions>(configuration.GetSection("Kinopoisk"));

        collection.AddScoped<IFilmsClient>(s =>
        {
            var options = s.GetRequiredService<IOptions<KinopoiskOptions>>();
            return new FilmsClient(
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