using System.Diagnostics;
using Cattobot.Db.Models.Enums;
using NetCord;
using NetCord.Gateway.Voice;
using NetCord.Rest;

namespace Cattobot.Models;

public class MusicPlayerContext
{
    public ulong GuildId { get; set; }
    public VoiceStateStatus Status { get; set; }
    public Process? EncodingProcess { get; set; }
    
    public TextChannel? TextChannel { get; set; }
    public RestMessage? PlayingNowMessage { get; set; }
    
    public ApplicationCommandInteraction? InteractionToReply { get; set; }
    
    public bool IsSkipped { get; set; }
}