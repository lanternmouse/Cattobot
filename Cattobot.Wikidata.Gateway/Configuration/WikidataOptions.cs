using System.ComponentModel.DataAnnotations;

namespace Cattobot.Wikidata.Gateway.Configuration;

public class WikidataOptions
{
    [Required] public string Url { get; set; } = "";
    [Required] public string Token { get; set; } = "";
}