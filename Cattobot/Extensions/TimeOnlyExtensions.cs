using System.Text;

namespace Cattobot.Extensions;

public static class TimeOnlyExtensions
{
    public static string ToNiceDuration(this TimeOnly timeOnly)
    {
        var result = new StringBuilder();

        if (timeOnly.Hour > 0)
        {
            result.Append(timeOnly.Hour).Append("ч ");
        }
        
        if (timeOnly.Hour > 0 || timeOnly.Minute > 0)
        {
            result.Append(timeOnly.Minute).Append("м ");
        }

        result.Append(timeOnly.Second).Append('с');

        return result.ToString();
    }
}