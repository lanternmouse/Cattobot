using Cattobot.Db.Models.Enums;
using Cattobot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Cattobot.AutocompleteHandlers;

public class NonPlannedFilmsAutocompleteHandler(
    IFilmRepository filmRepo
) : IAutocompleteProvider<AutocompleteInteractionContext>
{
    public async ValueTask<IEnumerable<ApplicationCommandOptionChoiceProperties>?> GetChoicesAsync(
        ApplicationCommandInteractionDataOption option, AutocompleteInteractionContext context)
    {
        var value = option.Value;

        var filmSuggestions = await filmRepo
            .GetGuildListQuery(context.Guild!.Id, null, [FilmStatus.Completed, FilmStatus.Abandoned], value)
            .Take(25)
            .Select(x => new {x.Film.LocalizedTitle, x.Film.Year, x.Film.Id})
            .ToListAsync();

        var results = filmSuggestions.Select(s => new ApplicationCommandOptionChoiceProperties(
            $"{s.LocalizedTitle} ({s.Year})",
            s.Id.ToString()
        ));

        return results.Take(25);
    }
}