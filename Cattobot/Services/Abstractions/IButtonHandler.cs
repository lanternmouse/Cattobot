using Discord.WebSocket;

namespace Cattobot.Services.Abstractions;

public interface IButtonHandler
{
    Task Handle(SocketMessageComponent component);
}