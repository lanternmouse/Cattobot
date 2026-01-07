using System.ComponentModel.DataAnnotations;

namespace YtDlp.Gateway.Configuration;

public class YtDlpOptions
{
    [Required] public string ExecutablePath { get; init; } = "";
    public string? CookiesFilePath { get; init; } = "";
}