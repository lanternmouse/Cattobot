using System.Diagnostics;
using Cattobot.Db.Models.Enums;
using NetCord;
using NetCord.Rest;

namespace Cattobot.Models;

public class MusicPlayerContext
{
    public ulong GuildId { get; set; }
    public MusicPlayerStatus Status { get; set; }
    public Process? EncodingProcess { get; set; }
    
    public TextChannel? TextChannel { get; set; }
    public RestMessage? PlayingNowMessage { get; set; }
    
    public ApplicationCommandInteraction? CommandInteractionToReply { get; set; }
    public ButtonInteraction? ButtonInteractionToReply { get; set; }
    
    public bool IsSkipped { get; set; }
}