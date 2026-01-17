using Newtonsoft.Json;

namespace Cattobot.Youtube.Gateway.Models;

public record YoutubeSearchResults
{
    [JsonProperty("contents")] public ResultContents Contents { get; init; } = new();
    
    public record ResultContents
    {
        [JsonProperty("sectionListRenderer")]
        public SectionListRenderer SectionListRenderer { get; init; } = new();
    }
    
    public record SectionListRenderer
    {
        [JsonProperty("contents")]
        public List<SectionListRendererContent> Contents { get; init; } = [];
    }
    
    public record SectionListRendererContent
    {
        [JsonProperty("itemSectionRenderer")]
        public ItemSectionRenderer ItemSectionRenderer { get; init; } = new();
    }
    
    public record ItemSectionRenderer
    {
        [JsonProperty("contents")]
        public List<ItemSectionRendererContent> Contents { get; init; } = new();
    }
    
    public record ItemSectionRendererContent
    {
        [JsonProperty("compactVideoRenderer")]
        public CompactVideoRenderer? CompactVideoRenderer { get; init; }
    }
    
    public record CompactVideoRenderer
    {
        [JsonProperty("videoId")]
        public string VideoId { get; init; }
    }
}
