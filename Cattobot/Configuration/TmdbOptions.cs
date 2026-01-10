using System.ComponentModel.DataAnnotations;

namespace Cattobot.Configuration;

public class TmdbOptions
{
    [Required] public string Token { get; set; }
}