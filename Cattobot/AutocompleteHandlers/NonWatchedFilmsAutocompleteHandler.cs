using Cattobot.Db.Models.Enums;
using Cattobot.Services.Abstractions;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.AutocompleteHandlers;

public class NonWatchedFilmsAutocompleteHandler(
    IFilmRepository filmRepo
) : AutocompleteHandler
{
    public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
    {
        var value = autocompleteInteraction.Data.Current.Value.ToString();

        var filmSuggestions = await filmRepo
            .GetGuildListQuery(context.Guild.Id, null, [FilmStatus.Planned, FilmStatus.Abandoned], value)
            .Take(25)
            .ToListAsync();

        var results = filmSuggestions.Select(s => new AutocompleteResult(
            $"{s.Film.LocalizedTitle} ({s.Film.Year})",
            s.Id.ToString()
        ));

        return AutocompletionResult.FromSuccess(results.Take(25));
    }
}