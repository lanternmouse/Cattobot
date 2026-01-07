using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YtDlp.Gateway.Configuration;
using YtDlp.Gateway.Services;
using YtDlp.Gateway.Services.Abstractions;

namespace YtDlp.Gateway;

public static class ServiceCollectionExtension
{
    public static void AddYtDlp(this IServiceCollection collection, IConfigurationRoot configuration)
    {
        collection.Configure<YtDlpOptions>(configuration.GetSection("YtDlp"));

        collection.AddScoped<IYtDlpService, YtDlpService>();
    }
}