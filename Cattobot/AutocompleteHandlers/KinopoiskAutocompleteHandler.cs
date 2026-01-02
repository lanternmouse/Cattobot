using Discord;
using Discord.Interactions;
using Kinopoisk.Gateway;

namespace Cattobot.AutocompleteHandlers;

public class KinopoiskAutocompleteHandler(
    IFilmsClient kinopoiskFilmsClient
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();
        if (value == null || value.Length < 3)
            return AutocompletionResult.FromSuccess([]);
        
        var filmSuggestions = await kinopoiskFilmsClient.SearchByKeywordAsync(value, 1);

        var results = filmSuggestions.Films.Select(s => new AutocompleteResult(
            $"{s.NameRu} ({s.Year}), {s.NameEn}",
            s.FilmId.ToString()
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}