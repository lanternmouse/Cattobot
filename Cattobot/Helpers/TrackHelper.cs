using Cattobot.Db.Models;

namespace Cattobot.Helpers;

public class TrackHelper
{
    public static string BuildTitleWithMarkdownUrl(TrackDb trackDb)
    {
        return !string.IsNullOrEmpty(trackDb.ExternalUrl)
            ? $"[{trackDb.Title}](<{trackDb.ExternalUrl}>)"
            : trackDb.Title;
    }
    
    public static string BuildAuthorWithMarkdownUrl(TrackDb trackDb)
    {
        return !string.IsNullOrEmpty(trackDb.ArtistUrl)
            ? $"[{trackDb.Artist}](<{trackDb.ArtistUrl}>)"
            : trackDb.Artist;
    }
}