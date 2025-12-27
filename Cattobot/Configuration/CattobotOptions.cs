using System.ComponentModel.DataAnnotations;

namespace Cattobot.Configuration;

public class CattobotOptions
{
    [Required] public string Token { get; set; }
}