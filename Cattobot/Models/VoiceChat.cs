using NetCord.Gateway.Voice;

namespace Cattobot.Models;

public record VoiceChat
{
    public ulong ChannelId { get; set; }
    public VoiceClient? VoiceClient { get; set; }
}