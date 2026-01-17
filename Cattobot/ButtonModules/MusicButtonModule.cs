using Cattobot.Db.Models.Enums;
using Cattobot.Services;
using Cattobot.Services.Abstractions;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace Cattobot.ButtonModules;

public class MusicButtonModule(
    ITrackQueueRepository trackQueueRepository,
    IMusicPlayerManager playerManager,
    IVoiceChatService voiceChatService
    ) : ComponentInteractionModule<ButtonInteractionContext>
{
    [ComponentInteraction("musicAdd")]
    public async Task Add(string incomingTrackId)
    {
        if (!await IsInSameChannel(Context)) return;
        
        var trackId = Guid.Parse(incomingTrackId);

        var queue = await trackQueueRepository.GetOrCreate(Context.Guild!.Id);

        var itemId = await trackQueueRepository.Append(queue.Id, trackId, Context.User.Id);

        var item = await trackQueueRepository.GetItem(itemId);

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredModifyMessage);
        await Context.Channel.SendMessageAsync(new MessageProperties()
            .WithContent($":cd: В очередь добавлен трек **{item!.Track.Title}**")
            .WithEmbeds([EmbedPropertiesProvider.GetTrackItemEmbed(item)])
            .WithComponents([ComponentsPropertiesProvider.AddedTrackItemComponents(item)])
        );
    }

    [ComponentInteraction("musicSkipTo")]
    public async Task MusicSkipTo(string incomingItemId, string incomingTrackId)
    {
        if (!await IsInSameChannel(Context)) return;
        
        var player = playerManager.GetOrCreate(Context.Guild!.Id);

        var itemId = Guid.Parse(incomingItemId);

        var item = await trackQueueRepository.GetItem(itemId);

        if (item == null)
        {
            var trackId = Guid.Parse(incomingTrackId!);
            var queue = await trackQueueRepository.GetOrCreate(Context.Guild!.Id);
            itemId = await trackQueueRepository.Append(queue.Id, trackId, Context.User.Id);
        }

        if (player.State.Status != MusicPlayerStatus.Stopped)
        {
            await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredModifyMessage);
            await player.SkipTo(itemId);
        }
        else
        {
            await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredModifyMessage);

            if (Context.Guild!.VoiceStates.TryGetValue(Context.User.Id, out var voiceState))
            {
                await voiceChatService.TryConnect(Context.Guild.Id, voiceState.ChannelId!.Value);
                player.SetTextChannel(Context.Channel);
                player.StartQueueIfStopped();
            }
        }
    }

    [ComponentInteraction("musicSkipForward")]
    public async Task MusicSkipForward()
    {
        if (!await IsInSameChannel(Context)) return;
        
        var player = playerManager.GetOrCreate(Context.Guild!.Id);
        player.SetButtonInteractionToFollowup(Context.Interaction);
        await player.SkipForward();
    }

    [ComponentInteraction("musicSkipBackward")]
    public async Task MusicSkipBackward()
    {
        if (!await IsInSameChannel(Context)) return;
        
        var player = playerManager.GetOrCreate(Context.Guild!.Id);
        player.SetButtonInteractionToFollowup(Context.Interaction);
        await player.SkipBackward();
    }

    [ComponentInteraction("musicResume")]
    public async Task MusicResume()
    {
        if (!await IsInSameChannel(Context)) return;
        
        var player = playerManager.GetOrCreate(Context.Guild!.Id);
        player.SetButtonInteractionToFollowup(Context.Interaction);
        await player.Resume();
    }
    
    [ComponentInteraction("musicPause")]
    public async Task MusicPause()
    {
        if (!await IsInSameChannel(Context)) return;
        
        var player = playerManager.GetOrCreate(Context.Guild!.Id);
        player.SetButtonInteractionToFollowup(Context.Interaction);
        await player.Pause();
    }
    
    private async Task<bool> IsInSameChannel(ButtonInteractionContext interactionContext)
    {
        if (!Context.Guild!.VoiceStates.TryGetValue(Context.User.Id, out var voiceState))
        {
            await interactionContext.Interaction.SendResponseAsync(
                InteractionCallback.Message(new InteractionMessageProperties()
                    .WithContent("Вы не подключены к голосовому каналу")
                    .WithFlags(MessageFlags.Ephemeral)));
            return false;
        }

        var botVoiceChannelId = voiceChatService.GetVoiceChannelId(Context.Guild.Id);
        if (botVoiceChannelId != null && botVoiceChannelId != voiceState.ChannelId)
        {
            await interactionContext.Interaction.SendResponseAsync(
                InteractionCallback.Message(new InteractionMessageProperties()
                    .WithContent("Вы не в одном голосовом канале с ботом")
                    .WithFlags(MessageFlags.Ephemeral)));
            return false;
        }

        return true;
    }
}