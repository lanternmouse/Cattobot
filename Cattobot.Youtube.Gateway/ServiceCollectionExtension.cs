using Cattobot.Youtube.Gateway.Configuration;
using Cattobot.Youtube.Gateway.Services;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cattobot.Youtube.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddYoutubeIntegration(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<YtDlpOptions>(configuration.GetSection("YtDlp"));

        collection.AddSingleton<IYoutubeService, YoutubeService>();
    }
}