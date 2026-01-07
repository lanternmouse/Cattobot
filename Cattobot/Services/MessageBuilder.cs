using NetCord.Rest;

namespace Cattobot.Services;

public static class MessagePropertyBuilder<T> where T : IMessageProperties, new()
{
    public static T Build() => new();
}