using NetCord;

namespace Cattobot.Services.Abstractions;

public interface IMusicPlayer
{
    void SetTextChannel(TextChannel textChannel);
    
    void SetInteractionToFollowup(ApplicationCommandInteraction interaction);
    
    void StartQueueIfStopped(ulong channelId, CancellationToken ct = default);

    Task SkipForward(CancellationToken ct = default);

    Task SkipBackward(CancellationToken ct = default);

    Task Resume(CancellationToken ct = default);

    Task Pause(CancellationToken ct = default);

    void Stop();
}