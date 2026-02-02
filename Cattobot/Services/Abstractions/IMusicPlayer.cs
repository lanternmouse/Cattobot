using Cattobot.Db.Models;
using NetCord;

namespace Cattobot.Services.Abstractions;

public interface IMusicPlayer
{
    void SetTextChannel(TextChannel textChannel);

    void SetCommandInteractionToFollowup(ApplicationCommandInteraction interaction);

    void SetButtonInteractionToFollowup(ButtonInteraction interaction);

    void StartQueueIfStopped(Guid? initialTrackItemId, CancellationToken ct = default);

    Task SkipTo(Guid? itemId, CancellationToken ct = default);

    Task SkipForward(CancellationToken ct = default);

    Task SkipBackward(CancellationToken ct = default);

    Task Resume(CancellationToken ct = default);

    Task Pause(CancellationToken ct = default);

    Task Stop();
}
