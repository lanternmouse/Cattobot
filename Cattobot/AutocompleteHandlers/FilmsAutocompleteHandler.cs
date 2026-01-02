using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.AutocompleteHandlers;

public class FilmsAutocompleteHandler(
    IFilmRepository filmRepo
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();

        var filmSuggestions = await filmRepo.GetListQuery(context.Guild.Id, context.User.Id)
            .Where(x => EF.Functions.ILike(x.LocalizedTitle, $"%{value}%"))
            .OrderByDescending(x => x.AddedOn)
            .Take(25)
            .ToListAsync();

        var results = filmSuggestions.Select(s => new AutocompleteResult(
            $"{s.LocalizedTitle} ({s.Year})",
            s.Id.ToString()
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}