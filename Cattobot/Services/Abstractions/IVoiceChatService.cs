using Cattobot.Models;
using NetCord.Gateway.Voice;

namespace Cattobot.Services.Abstractions;

public interface IVoiceChatService
{
    VoiceClient? GetVoiceClient(ulong guildId);
    ulong? GetVoiceChannelId(ulong guildId);
    Task<VoiceChat> TryConnect(ulong guildId, ulong channelId);
    Task Disconnect(ulong guildId);
}