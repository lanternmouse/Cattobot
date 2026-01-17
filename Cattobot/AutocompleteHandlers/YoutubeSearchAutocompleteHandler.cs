using Cattobot.Youtube.Gateway.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Cattobot.AutocompleteHandlers;

public class YoutubeSearchAutocompleteHandler(
    IYoutubeService youtubeService,
    IMemoryCache cache
) : IAutocompleteProvider<AutocompleteInteractionContext>
{
    public async ValueTask<IEnumerable<ApplicationCommandOptionChoiceProperties>?> GetChoicesAsync(
        ApplicationCommandInteractionDataOption option, AutocompleteInteractionContext context)
    {
        var value = option.Value;
        
        if (value == null || value.Length < 2) return [];

        var cacheKey = $"youtube-search-{value}";
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(3));

        IEnumerable<string> suggestions;
        if (cache.TryGetValue(cacheKey, out IEnumerable<string>? result))
        {
            suggestions = result!;
        }
        else
        {
            suggestions = await youtubeService.GetYoutubeSearchSuggestions(value);
            suggestions = suggestions.ToArray();

            cache.Set(cacheKey, suggestions, cacheOptions);
        }

        var results = suggestions
            .Select(s => new ApplicationCommandOptionChoiceProperties(s, s));

        return results.Take(25);
    }
}