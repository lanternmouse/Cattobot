namespace Cattobot.Extensions;

public static class StringExtensions
{
    public static string LimitWithEllipsis(this string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength) return text;

        return string.Concat(text.AsSpan(0, maxLength - 3), "...");
    }
}