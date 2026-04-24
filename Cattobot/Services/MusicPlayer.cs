using Cattobot.Db.Models;
using Cattobot.Db.Models.Enums;
using Cattobot.Extensions;
using Cattobot.Helpers;
using Cattobot.Models;
using Cattobot.Services.Abstractions;
using Cattobot.Youtube.Gateway.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCord;
using NetCord.Gateway.Voice;
using NetCord.Rest;

namespace Cattobot.Services;

public class MusicPlayer(
    IYoutubeService youtubeService,
    ILogger<MusicPlayer> logger,
    IServiceScopeFactory serviceScopeFactory,
    IVoiceChatService voiceChatService)
    : IMusicPlayer
{
    public MusicPlayerContext State { get; set; } = new();

    private CancellationTokenSource TrackCancellationTokenSource { get; set; } = new();

    public void StartQueueIfStopped(Guid? initialTrackItemId, CancellationToken ct = default)
    {
        if (State.Status != MusicPlayerStatus.Stopped) return;
        _ = Task.Run(async () => await StartQueue(initialTrackItemId, ct), ct);
    }

    public void SetTextChannel(TextChannel textChannel)
    {
        State.TextChannel = textChannel;
    }

    public void SetCommandInteractionToFollowup(ApplicationCommandInteraction interaction)
    {
        State.CommandInteractionToReply = interaction;
    }

    public void SetButtonInteractionToFollowup(ButtonInteraction interaction)
    {
        State.ButtonInteractionToReply = interaction;
    }

    private async Task StartQueue(Guid? initialTrackItemId, CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var voiceClient = voiceChatService.GetVoiceClient(State.GuildId);

        if (voiceClient == null) return;
        
        await using var voiceStream = voiceClient.CreateVoiceStream();

        await using var opusStream = new OpusEncodeStream(voiceStream, PcmFormat.Short, VoiceChannels.Stereo,
            OpusApplication.Audio);

        State.Status = MusicPlayerStatus.Playing;

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);

        if (initialTrackItemId != null)
        {
            await queueRepo.SetCurrentItem(queue.Id, initialTrackItemId.Value, ct);
        }

        var currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);

        if (currentItem == null)
        {
            currentItem = await queueRepo.GetFirstItem(queue.Id, ct);
            await queueRepo.SetCurrentItem(queue.Id, currentItem?.Id, ct);
        }

        try
        {
            while (State.Status != MusicPlayerStatus.Stopped && voiceClient != null)
            {
                State.Status = MusicPlayerStatus.Playing;

                if (currentItem == null) break;

                await SendPlayingNowMessage(currentItem);

                try
                {
                    TrackCancellationTokenSource.Dispose();
                    TrackCancellationTokenSource = new CancellationTokenSource();

                    await PlayTrack(opusStream, currentItem, TrackCancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
                catch (Exception ex)
                {
                    await SendErrorMessage(currentItem, ex.Message);
                }

                currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);

                if (!State.IsSkipped)
                {
                    await queueRepo.SetCurrentItem(queue.Id, currentItem?.NextItemId, ct);
                    currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);
                }

                if (State.IsSkipped) State.IsSkipped = false;

                voiceClient = voiceChatService.GetVoiceClient(State.GuildId);

                ct.ThrowIfCancellationRequested();
            }
        }
        finally
        {
            await queueRepo.SetCurrentItem(queue.Id, null, ct);
            if (State.PlayingNowMessage != null) await DeletePlayingNowMessage();
            State.PlayingNowMessage = null;
            State.Status = MusicPlayerStatus.Stopped;
        }
    }

    public async Task SkipTo(Guid? itemId, CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);
        await queueRepo.SetCurrentItem(queue.Id, itemId, ct);

        State.IsSkipped = true;
        
        await TrackCancellationTokenSource.CancelAsync();
    }

    public async Task SkipForward(CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);
        if (queue.CurrentTrackId == null) return;
        var currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);
        
        await SkipTo(currentItem?.NextItemId, ct);
    }

    public async Task SkipBackward(CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);
        if (queue.CurrentTrackId == null) return;

        var currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);

        await SkipTo(currentItem?.PrevItemId, ct);
    }

    public async Task Resume(CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);
        if (queue.CurrentTrackId == null) return;

        var currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);
        if (currentItem == null) return;

        State.EncodingProcess?.SendSignalContinue();
        if (State.Status == MusicPlayerStatus.Paused) State.Status = MusicPlayerStatus.Playing;

        await SendPlayingNowMessage(currentItem);
    }

    public async Task Pause(CancellationToken ct = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var queueRepo = scope.ServiceProvider.GetRequiredService<ITrackQueueRepository>();

        var queue = await queueRepo.GetOrCreate(State.GuildId, ct);
        if (queue.CurrentTrackId == null) return;

        var currentItem = await queueRepo.GetCurrentItem(queue.Id, ct);
        if (currentItem == null) return;

        State.EncodingProcess?.SendSignalStop();
        if (State.Status == MusicPlayerStatus.Playing) State.Status = MusicPlayerStatus.Paused;

        await SendPlayingNowMessage(currentItem);
    }

    public async Task Stop()
    {
        State.Status = MusicPlayerStatus.Stopped;
        await TrackCancellationTokenSource.CancelAsync();
        CloseEncodingProcess();
    }

    private async Task PlayTrack(Stream opusStream, TrackQueueItemDb trackItem, CancellationToken ct = default)
    {
        CloseEncodingProcess();

        var sourceUrl = await youtubeService.GetAudioStreamUrl(trackItem.Track.ExternalUrl, ct);

        State.EncodingProcess = FFmpegProvider.StartEncodeProcess(sourceUrl);

        try
        {
            await State.EncodingProcess.StandardOutput.BaseStream.CopyToAsync(opusStream, ct);
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
        catch (Exception ex)
        {
            await SendErrorMessage(trackItem, ex.Message);
        }

        await opusStream.FlushAsync(CancellationToken.None);

        CloseEncodingProcess();
    }

    private async Task SendPlayingNowMessage(TrackQueueItemDb trackItem)
    {
        try
        {
            var content = State.Status == MusicPlayerStatus.Paused
                ? $":notes: **НА ПАУЗЕ** ~~Сейчас играет {trackItem.Track.Title}~~"
                : $":notes: Сейчас играет **{trackItem.Track.Title}**";

            State.PlayingNowMessage = await SendMessage(new MessageProperties()
                    .WithContent(content)
                    .WithEmbeds([EmbedPropertiesProvider.GetTrackItemEmbed(trackItem)])
                    .WithComponents([ComponentsPropertiesProvider.MusicPlayerComponents(State.Status)]),
                CancellationToken.None);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in SendPlayingNowMessage");
        }
    }

    private async Task DeletePlayingNowMessage()
    {
        if (State.PlayingNowMessage != null)
        {
            await State.PlayingNowMessage.DeleteAsync();
            State.PlayingNowMessage = null;
        }
    }

    private async Task SendErrorMessage(TrackQueueItemDb trackItem, string errorMessage)
    {
        if (State.TextChannel != null)
        {
            await SendMessage(new MessageProperties()
                    .WithContent($"Ошибка воспроизведения трека " +
                                 $"**{TrackHelper.BuildTitleWithMarkdownUrl(trackItem.Track)}**" +
                                 $"\n{errorMessage}"),
                CancellationToken.None);
        }
    }

    private async Task<RestMessage?> SendMessage(MessageProperties messageProperties, CancellationToken ct)
    {
        RestMessage? message = null;
        if (State.CommandInteractionToReply != null)
        {
            await DeletePlayingNowMessage();
            message = await State.CommandInteractionToReply.SendFollowupMessageAsync(new InteractionMessageProperties()
                .WithContent(messageProperties.Content)
                .WithEmbeds(messageProperties.Embeds)
                .AddComponents(messageProperties.Components ?? []), cancellationToken: ct);
            State.CommandInteractionToReply = null;
        }
        else if (State.ButtonInteractionToReply != null)
        {
            if (State.PlayingNowMessage == null)
                State.PlayingNowMessage = await State.TextChannel!.SendMessageAsync(messageProperties, cancellationToken: ct);

            message = await State.PlayingNowMessage.ModifyAsync(a =>
                a.WithContent(messageProperties.Content)
                    .WithEmbeds(messageProperties.Embeds)
                    .AddComponents(messageProperties.Components ?? []), cancellationToken: ct);
            await State.ButtonInteractionToReply.SendResponseAsync(InteractionCallback.DeferredModifyMessage,
                cancellationToken: ct);
            State.ButtonInteractionToReply = null;
        }
        else if (State.TextChannel != null)
        {
            await DeletePlayingNowMessage();
            message = await State.TextChannel.SendMessageAsync(messageProperties, cancellationToken: ct);
        }

        return message;
    }

    private void CloseEncodingProcess()
    {
        try
        {
            State.EncodingProcess?.SendSignalContinue();
            State.EncodingProcess?.SendSignalClose();
        }
        catch
        {
            // ignored
        }

        State.EncodingProcess?.Dispose();
        State.EncodingProcess = null;
    }
}
