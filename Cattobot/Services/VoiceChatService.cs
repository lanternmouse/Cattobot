using Cattobot.Services.Abstractions;
using NetCord.Gateway;
using NetCord.Gateway.Voice;

namespace Cattobot.Services;

public class VoiceChatService : IVoiceChatService
{
    private readonly GatewayClient _discordClient;
    private readonly IMusicPlayerManager _musicPlayerManager;
    private readonly Dictionary<ulong, VoiceClient> _voiceClients = [];
    
    public VoiceChatService(GatewayClient discordClient, IMusicPlayerManager musicPlayerManager)
    {
        discordClient.VoiceStateUpdate += HandleVoiceStateUpdate;
        
        _musicPlayerManager = musicPlayerManager;
        _discordClient = discordClient;
    }

    public VoiceClient? GetVoiceClient(ulong guildId)
    {
        return _voiceClients.GetValueOrDefault(guildId);
    }
    
    public async Task<VoiceClient> TryConnect(ulong guildId, ulong channelId)
    {
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token;
        
        if (!_voiceClients.TryGetValue(guildId, out var voiceClient))
        {
            voiceClient = await _discordClient.JoinVoiceChannelAsync(guildId, channelId,
                cancellationToken: cancellationToken);
            _voiceClients[guildId] = voiceClient;

            await voiceClient.StartAsync(cancellationToken);
            await voiceClient.EnterSpeakingStateAsync(new SpeakingProperties(SpeakingFlags.Microphone),
                cancellationToken: cancellationToken);
            
            return voiceClient;
        }

        await _discordClient.UpdateVoiceStateAsync(new VoiceStateProperties(guildId, channelId),
            cancellationToken: cancellationToken);

        return voiceClient;
    }
    
    public async Task Disconnect(ulong guildId)
    {
        await _discordClient.UpdateVoiceStateAsync(new VoiceStateProperties(guildId, null));
        _voiceClients.Remove(guildId);
    }

    private ValueTask HandleVoiceStateUpdate(VoiceState state)
    {
        if (state.ChannelId == null)
        {
            _voiceClients.Remove(state.GuildId);
            _musicPlayerManager.Drop(state.GuildId);
        }

        return ValueTask.CompletedTask;
    }
}