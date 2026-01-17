using NetCord.Gateway.Voice;

namespace Cattobot.Services.Abstractions;

public interface IVoiceChatService
{
    VoiceClient? GetVoiceClient(ulong guildId);
    Task<VoiceClient> TryConnect(ulong guildId, ulong channelId);
    Task Disconnect(ulong guildId);
}