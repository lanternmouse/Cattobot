using Microsoft.Extensions.Caching.Memory;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Cattobot.AutocompleteHandlers;

public class FilmSearchAutocompleteHandler(
    TMDbClient tmdbClient,
    IMemoryCache cache
) : IAutocompleteProvider<AutocompleteInteractionContext>
{
    public async ValueTask<IEnumerable<ApplicationCommandOptionChoiceProperties>?> GetChoicesAsync(
        ApplicationCommandInteractionDataOption option, AutocompleteInteractionContext context)
    {
        var value = option.Value;
        
        if (value == null || value.Length < 2) return [];

        var cacheKey = $"film-search-{value}";
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(3));

        SearchContainer<SearchMovie?> filmSuggestions;
        if (cache.TryGetValue(cacheKey, out SearchContainer<SearchMovie?>? result))
        {
            filmSuggestions = result!;
        }
        else
        {
            filmSuggestions = await tmdbClient.SearchMovieAsync(value, 1);
            cache.Set(cacheKey, filmSuggestions, cacheOptions);
            foreach (var filmSuggestion in filmSuggestions.Results)
            {
                if (filmSuggestion == null) continue;
                
                var filmCacheKey = $"tmdb-{filmSuggestion.Id}";
                if (!cache.TryGetValue(filmCacheKey, out _))
                {
                    cache.Set(filmCacheKey, filmSuggestion, cacheOptions);
                }
            }
        }

        var results = filmSuggestions.Results
            .Where(s => s != null)
            .Select(s =>
            {
                var title = s!.Title;
                if (s.ReleaseDate.HasValue) title += $" ({s.ReleaseDate.Value.Year})";
                return new ApplicationCommandOptionChoiceProperties(
                    title,
                    s.Id.ToString()
                );
            });

        return results.Take(25);
    }
}