using Cattobot.Db.Models.Enums;
using Cattobot.Models;
using Cattobot.Services.Abstractions;

namespace Cattobot.Services;

public class MusicPlayerManager(
    IServiceProvider serviceProvider)
    : IMusicPlayerManager
{
    private Dictionary<ulong, MusicPlayer> MusicPlayers { get; set; } = new();

    public MusicPlayer GetOrCreate(ulong guildId)
    {
        if (MusicPlayers.TryGetValue(guildId, out var player))
            return player;

        player = serviceProvider.GetService(typeof(IMusicPlayer)) as MusicPlayer;

        player!.State = new MusicPlayerContext
        {
            GuildId = guildId,
            Status = VoiceStateStatus.Stopped
        };

        MusicPlayers[guildId] = player;

        return player;
    }
    
    public void Drop(ulong guildId)
    {
        if (MusicPlayers.TryGetValue(guildId, out var player))
        {
            player.Stop();
        }

        MusicPlayers.Remove(guildId, out _);
    }
}