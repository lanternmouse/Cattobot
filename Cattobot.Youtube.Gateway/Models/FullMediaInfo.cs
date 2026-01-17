using Newtonsoft.Json;

namespace Cattobot.Youtube.Gateway.Models;

public class FullMediaInfo
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("formats")] public List<FormatInfo> Formats { get; set; }

    [JsonProperty("thumbnails")] public List<ThumbnailInfo> Thumbnails { get; set; }

    [JsonProperty("thumbnail")] public string Thumbnail { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("channel_id")] public string ChannelId { get; set; }

    [JsonProperty("channel_url")] public string ChannelUrl { get; set; }

    [JsonProperty("duration")] public int Duration { get; set; }

    [JsonProperty("webpage_url")] public string WebpageUrl { get; set; }

    [JsonProperty("live_status")] public string LiveStatus { get; set; }

    [JsonProperty("media_type")] public string MediaType { get; set; }
    [JsonProperty("channel")] public string Channel { get; set; }

    [JsonProperty("uploader")] public string Uploader { get; set; }

    [JsonProperty("uploader_id")] public string UploaderId { get; set; }

    [JsonProperty("uploader_url")] public string UploaderUrl { get; set; }

    [JsonProperty("upload_date")] public string UploadDate { get; set; }
    
    [JsonProperty("original_url")] public string OriginalUrl { get; set; }

    [JsonProperty("webpage_url_basename")] public string WebpageUrlBasename { get; set; }

    [JsonProperty("webpage_url_domain")] public string WebpageUrlDomain { get; set; }

    [JsonProperty("extractor")] public string Extractor { get; set; }

    [JsonProperty("extractor_key")] public string ExtractorKey { get; set; }

    [JsonProperty("playlist")] public object Playlist { get; set; }

    [JsonProperty("playlist_index")] public object PlaylistIndex { get; set; }

    [JsonProperty("is_live")] public bool IsLive { get; set; }

    public class FormatInfo
    {
        [JsonProperty("format_id")] public string FormatId { get; set; }

        [JsonProperty("format_note")] public string FormatNote { get; set; }

        [JsonProperty("ext")] public string Ext { get; set; }

        [JsonProperty("protocol")] public string Protocol { get; set; }

        [JsonProperty("acodec")] public string Acodec { get; set; }

        [JsonProperty("vcodec")] public string Vcodec { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("width")] public int? Width { get; set; }

        [JsonProperty("height")] public int? Height { get; set; }

        [JsonProperty("fps")] public double? Fps { get; set; }

        [JsonProperty("rows")] public int Rows { get; set; }

        [JsonProperty("columns")] public int Columns { get; set; }

        [JsonProperty("audio_ext")] public string AudioExt { get; set; }

        [JsonProperty("video_ext")] public string VideoExt { get; set; }

        [JsonProperty("vbr")] public double? Vbr { get; set; }

        [JsonProperty("abr")] public double? Abr { get; set; }

        [JsonProperty("tbr")] public double? Tbr { get; set; }

        [JsonProperty("resolution")] public string Resolution { get; set; }

        [JsonProperty("aspect_ratio")] public double? AspectRatio { get; set; }

        [JsonProperty("filesize_approx")] public int? FilesizeApprox { get; set; }

        [JsonProperty("format")] public string Format { get; set; }

        [JsonProperty("asr")] public int? Asr { get; set; }

        [JsonProperty("filesize")] public int? Filesize { get; set; }

        [JsonProperty("source_preference")] public int? SourcePreference { get; set; }

        [JsonProperty("audio_channels")] public int? AudioChannels { get; set; }

        [JsonProperty("quality")] public double? Quality { get; set; }

        [JsonProperty("has_drm")] public bool? HasDrm { get; set; }

        [JsonProperty("language")] public object Language { get; set; }

        [JsonProperty("language_preference")] public int? LanguagePreference { get; set; }

        [JsonProperty("preference")] public object Preference { get; set; }

        [JsonProperty("dynamic_range")] public string DynamicRange { get; set; }

        [JsonProperty("container")] public string Container { get; set; }

        [JsonProperty("available_at")] public int? AvailableAt { get; set; }

        [JsonProperty("format_index")] public object FormatIndex { get; set; }

        [JsonProperty("manifest_url")] public string ManifestUrl { get; set; }
    }

    public class ThumbnailInfo
    {
        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("preference")] public int Preference { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("height")] public int? Height { get; set; }

        [JsonProperty("width")] public int? Width { get; set; }

        [JsonProperty("resolution")] public string Resolution { get; set; }
    }
}
