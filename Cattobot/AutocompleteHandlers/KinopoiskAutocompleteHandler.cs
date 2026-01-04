using Discord;
using Discord.Interactions;
using Kinopoisk.Gateway;
using Microsoft.Extensions.Caching.Memory;

namespace Cattobot.AutocompleteHandlers;

public class KinopoiskAutocompleteHandler(
    IFilmsClient kinopoiskFilmsClient,
    IMemoryCache cache
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();
        if (value == null || value.Length < 2)
            return AutocompletionResult.FromSuccess([]);

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

        var results = filmSuggestions.Films.Select(s => new AutocompleteResult(
            $"{s.NameRu} ({s.Year}), {s.NameEn}",
            s.FilmId.ToString()
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}