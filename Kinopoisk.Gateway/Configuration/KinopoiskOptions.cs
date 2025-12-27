using System.ComponentModel.DataAnnotations;

namespace Kinopoisk.Gateway.Configuration;

public class KinopoiskOptions
{
    [Required] public string Url { get; set; }
    [Required] public string Token { get; set; }
}