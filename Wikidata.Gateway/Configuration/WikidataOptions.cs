using System.ComponentModel.DataAnnotations;

namespace Wikidata.Gateway.Configuration;

public class WikidataOptions
{
    [Required] public string Url { get; set; } = "";
    [Required] public string Token { get; set; } = "";
}