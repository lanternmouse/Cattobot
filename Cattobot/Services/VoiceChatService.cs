using Cattobot.Models;
using Cattobot.Services.Abstractions;
using NetCord.Gateway;
using NetCord.Gateway.Voice;

namespace Cattobot.Services;

public class VoiceChatService : IVoiceChatService
{
    private readonly GatewayClient _discordClient;
    private readonly IMusicPlayerManager _musicPlayerManager;
    private readonly Dictionary<ulong, VoiceChat> _voiceClients = [];
    
    public VoiceChatService(GatewayClient discordClient, IMusicPlayerManager musicPlayerManager)
    {
        discordClient.VoiceStateUpdate += HandleVoiceStateUpdate;
        
        _musicPlayerManager = musicPlayerManager;
        _discordClient = discordClient;
    }

    public VoiceClient? GetVoiceClient(ulong guildId)
    {
        return _voiceClients.GetValueOrDefault(guildId)?.VoiceClient;
    }
    
    public ulong? GetVoiceChannelId(ulong guildId)
    {
        return _voiceClients.GetValueOrDefault(guildId)?.ChannelId;
    }
    
    public async Task<VoiceChat> TryConnect(ulong guildId, ulong channelId)
    {
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token;
        
        if (!_voiceClients.TryGetValue(guildId, out var voiceChat))
        {
            voiceChat = new VoiceChat
            {
                ChannelId = channelId,
                VoiceClient = null,
            };
            
            _voiceClients[guildId] = voiceChat;

            var voiceClient = await _discordClient.JoinVoiceChannelAsync(guildId, channelId,
                cancellationToken: cancellationToken);

            await voiceClient.StartAsync(cancellationToken);
            await voiceClient.EnterSpeakingStateAsync(new SpeakingProperties(SpeakingFlags.Microphone),
                cancellationToken: cancellationToken);
            
            voiceChat.VoiceClient = voiceClient;
            
            return voiceChat;
        }

        await _discordClient.UpdateVoiceStateAsync(new VoiceStateProperties(guildId, channelId),
            cancellationToken: cancellationToken);
        voiceChat.ChannelId = channelId;

        return voiceChat;
    }
    
    public async Task Disconnect(ulong guildId)
    {
        await _discordClient.UpdateVoiceStateAsync(new VoiceStateProperties(guildId, null));
        _voiceClients.Remove(guildId);
    }

    private async ValueTask HandleVoiceStateUpdate(VoiceState state)
    {
        if (state.UserId != _discordClient.Id) return;

        if (state.ChannelId == null)
        {
            _voiceClients.Remove(state.GuildId);
            _musicPlayerManager.Drop(state.GuildId);
        }
        else
        {
            if (!_voiceClients.TryGetValue(state.GuildId, out var voiceClient))
            {
                await Disconnect(state.GuildId);
                return;
            }

            if (voiceClient.ChannelId != state.ChannelId)
            {
                _voiceClients.Remove(state.GuildId);

                await TryConnect(state.GuildId, state.ChannelId.Value);
                
                voiceClient.VoiceClient?.Dispose();
            }
        }
    }
}