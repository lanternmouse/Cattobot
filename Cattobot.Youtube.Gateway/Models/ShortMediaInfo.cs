using Newtonsoft.Json;

namespace Cattobot.Youtube.Gateway.Models;

public record ShortMediaInfo
{
    [JsonProperty("_type")] public string Type { get; set; }

    [JsonProperty("ie_key")] public string IeKey { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("url")] public string Url { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("description")] public object Description { get; set; }

    [JsonProperty("duration")] public double Duration { get; set; }

    [JsonProperty("channel_id")] public string ChannelId { get; set; }

    [JsonProperty("channel")] public string Channel { get; set; }

    [JsonProperty("channel_url")] public string ChannelUrl { get; set; }

    [JsonProperty("uploader")] public string Uploader { get; set; }

    [JsonProperty("uploader_id")] public string UploaderId { get; set; }

    [JsonProperty("uploader_url")] public string UploaderUrl { get; set; }

    [JsonProperty("thumbnails")] public List<Thumbnail> Thumbnails { get; set; }

    [JsonProperty("timestamp")] public object Timestamp { get; set; }

    [JsonProperty("release_timestamp")] public object ReleaseTimestamp { get; set; }

    [JsonProperty("availability")] public object Availability { get; set; }

    [JsonProperty("view_count")] public int ViewCount { get; set; }

    [JsonProperty("live_status")] public object LiveStatus { get; set; }

    [JsonProperty("channel_is_verified")] public object ChannelIsVerified { get; set; }

    [JsonProperty("__x_forwarded_for_ip")] public object XForwardedForIp { get; set; }

    [JsonProperty("webpage_url")] public string WebpageUrl { get; set; }

    [JsonProperty("original_url")] public string OriginalUrl { get; set; }

    [JsonProperty("webpage_url_basename")] public string WebpageUrlBasename { get; set; }

    [JsonProperty("webpage_url_domain")] public string WebpageUrlDomain { get; set; }

    [JsonProperty("extractor")] public string Extractor { get; set; }

    [JsonProperty("extractor_key")] public string ExtractorKey { get; set; }

    [JsonProperty("playlist_count")] public int PlaylistCount { get; set; }

    [JsonProperty("playlist")] public string Playlist { get; set; }

    [JsonProperty("playlist_id")] public string PlaylistId { get; set; }

    [JsonProperty("playlist_title")] public string PlaylistTitle { get; set; }

    [JsonProperty("playlist_uploader")] public object PlaylistUploader { get; set; }

    [JsonProperty("playlist_uploader_id")] public object PlaylistUploaderId { get; set; }

    [JsonProperty("playlist_channel")] public object PlaylistChannel { get; set; }

    [JsonProperty("playlist_channel_id")] public object PlaylistChannelId { get; set; }

    [JsonProperty("playlist_webpage_url")] public string PlaylistWebpageUrl { get; set; }

    [JsonProperty("n_entries")] public int NEntries { get; set; }

    [JsonProperty("playlist_index")] public int PlaylistIndex { get; set; }

    [JsonProperty("__last_playlist_index")]
    public int LastPlaylistIndex { get; set; }

    [JsonProperty("playlist_autonumber")] public int PlaylistAutonumber { get; set; }

    [JsonProperty("epoch")] public int Epoch { get; set; }

    [JsonProperty("duration_string")] public string DurationString { get; set; }

    [JsonProperty("release_year")] public object ReleaseYear { get; set; }
    
    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}