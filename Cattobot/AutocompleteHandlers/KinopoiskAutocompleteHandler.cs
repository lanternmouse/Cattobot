using Kinopoisk.Gateway;
using Microsoft.Extensions.Caching.Memory;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Cattobot.AutocompleteHandlers;

public class KinopoiskAutocompleteProvider(
    IFilmsClient kinopoiskFilmsClient,
    IMemoryCache cache
) : IAutocompleteProvider<AutocompleteInteractionContext>
{
    public async ValueTask<IEnumerable<ApplicationCommandOptionChoiceProperties>?> GetChoicesAsync(
        ApplicationCommandInteractionDataOption option, AutocompleteInteractionContext context)
    {
        var value = option.Value;
        
        if (value == null || value.Length < 2) return [];

        var cacheKey = $"kinopoisk-search-{value}";
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(3));

        FilmSearchResponse filmSuggestions;
        if (cache.TryGetValue(cacheKey, out FilmSearchResponse? result))
        {
            filmSuggestions = result!;
        }
        else
        {
            filmSuggestions = await kinopoiskFilmsClient.SearchByKeywordAsync(value, 1);
            cache.Set(cacheKey, filmSuggestions.Films, cacheOptions);
            foreach (var filmSuggestion in filmSuggestions.Films)
            {
                var filmCacheKey = $"kinopoisk-{filmSuggestion.FilmId}";
                if (!cache.TryGetValue(filmCacheKey, out _))
                {
                    cache.Set(filmCacheKey, filmSuggestion, cacheOptions);
                }
            }
        }

        var results = filmSuggestions.Films.Select(s => new ApplicationCommandOptionChoiceProperties(
            $"{s.NameRu} ({s.Year}), {s.NameEn}",
            s.FilmId.ToString()
        ));

        return results.Take(25);
    }
}