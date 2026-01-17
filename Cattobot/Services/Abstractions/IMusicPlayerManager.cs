namespace Cattobot.Services.Abstractions;

public interface IMusicPlayerManager
{
    MusicPlayer GetOrCreate(ulong guildId);

    void Drop(ulong guildId);
}