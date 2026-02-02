namespace Cattobot.Services.Abstractions;

public interface IMusicPlayerManager
{
    MusicPlayer GetOrCreate(ulong guildId);

    Task Drop(ulong guildId);
}