using System.ComponentModel.DataAnnotations;

namespace Cattobot.Configuration;

public class FilmsOptions
{
    [Required] public string KinopoiskWatchLink { get; init; } = "";
}