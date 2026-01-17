using Cattobot.AutocompleteHandlers;
using Cattobot.Db.Models.Enums;
using Cattobot.Helpers;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Cattobot.CommandModules;

public class MusicCommandModule(
    IMusicPlayerManager musicPlayerManager,
    ITrackQueueService trackQueueService,
    ITrackQueueRepository trackQueueRepo,
    IVoiceChatService voiceChatService
    ) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("play", "Воспроизвести трек или добавить в очередь")]
    public async Task Play(
        [SlashCommandParameter(AutocompleteProviderType = typeof(YoutubeSearchAutocompleteHandler))] string query)
    {
        var guild = Context.Guild!;
        
        // Get the user voice state
        if (!guild.VoiceStates.TryGetValue(Context.User.Id, out var voiceState))
        {
            await RespondAsync(InteractionCallback.Message(new InteractionMessageProperties()
                .WithContent("Вы не подключены к голосовому каналу")
                .WithFlags(MessageFlags.Ephemeral)));
            return;
        }

        await Task.Delay(50);
        
        await RespondAsync(InteractionCallback.DeferredMessage());

        try
        {
            await voiceChatService.TryConnect(Context.Guild!.Id, voiceState.ChannelId!.Value);
            
            var trackItem = await trackQueueService.EnqueueFromQuery(guild.Id, Context.User.Id, query);

            var player = musicPlayerManager.GetOrCreate(guild.Id);
            player.SetTextChannel(Context.Channel);
            player.StartQueueIfStopped();

            await FollowupAsync(new InteractionMessageProperties()
                .WithContent($":cd: В очередь добавлен трек **{trackItem.Track.Title}**")
                .WithEmbeds([EmbedPropertiesProvider.GetTrackItemEmbed(trackItem)])
                .WithComponents([ComponentsPropertiesProvider.AddedTrackItemComponents(trackItem)]));
        } catch (Exception e)
        {
            await FollowupAsync(new InteractionMessageProperties().WithContent(e.Message));
        }
    }
    
    [SlashCommand("skip", "Воспроизвести следующий трек в очереди")]
    public async Task Skip()
    {
        var player = musicPlayerManager.GetOrCreate(Context.Guild!.Id);

        if (player.State.Status == MusicPlayerStatus.Stopped)
        {
            await RespondAsync(InteractionCallback.Message("Плеер не активен"));
        }

        await RespondAsync(InteractionCallback.DeferredMessage());
        
        player.SetCommandInteractionToFollowup(Context.Interaction);
        await player.SkipForward();
    }
    
    [SlashCommand("skip-backward", "Воспроизвести предыдущий трек в очереди")]
    public async Task Previous()
    {
        var player = musicPlayerManager.GetOrCreate(Context.Guild!.Id);
        
        if (player.State.Status == MusicPlayerStatus.Stopped)
        {
            await RespondAsync(InteractionCallback.Message("Плеер не активен"));
        }
        
        await RespondAsync(InteractionCallback.DeferredMessage());
        
        player.SetCommandInteractionToFollowup(Context.Interaction);
        await player.SkipBackward();
    }
    
    [SlashCommand("pause", "Приостановить воспроизведение")]
    public async Task Pause()
    {
        var player = musicPlayerManager.GetOrCreate(Context.Guild!.Id);
        
        await RespondAsync(InteractionCallback.DeferredMessage());
        
        player.SetCommandInteractionToFollowup(Context.Interaction);
        await player.Pause();
    }
    
    [SlashCommand("resume", "Возобновить воспроизведение")]
    public async Task Resume()
    {
        var player = musicPlayerManager.GetOrCreate(Context.Guild!.Id);
        
        await RespondAsync(InteractionCallback.DeferredMessage());
        
        player.SetCommandInteractionToFollowup(Context.Interaction);
        await player.Resume();
    }

    [SlashCommand("stop", "Остановить воспроизведение и очистить очередь")]
    public async Task Stop()
    {
        await trackQueueRepo.Drop(Context.Guild!.Id);
        await voiceChatService.Disconnect(Context.Guild!.Id);

        await RespondAsync(InteractionCallback.Message("Воспроизведение остановлено и очередь очищена"));
    }
}