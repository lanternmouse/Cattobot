using Newtonsoft.Json;

namespace Cattobot.Youtube.Gateway.Models;

public class YoutubeVideoInfo
{
    public class Accessibility
    {
        [JsonProperty("accessibilityData")]
        public AccessibilityData AccessibilityData { get; set; }
    }

    public class AccessibilityData
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class AdaptiveFormat
    {
        [JsonProperty("itag")]
        public int Itag { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("bitrate")]
        public int Bitrate { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("initRange")]
        public InitRange InitRange { get; set; }

        [JsonProperty("indexRange")]
        public IndexRange IndexRange { get; set; }

        [JsonProperty("lastModified")]
        public string LastModified { get; set; }

        [JsonProperty("contentLength")]
        public string ContentLength { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("fps")]
        public int Fps { get; set; }

        [JsonProperty("qualityLabel")]
        public string QualityLabel { get; set; }

        [JsonProperty("projectionType")]
        public string ProjectionType { get; set; }

        [JsonProperty("averageBitrate")]
        public int AverageBitrate { get; set; }

        [JsonProperty("highReplication")]
        public bool HighReplication { get; set; }

        [JsonProperty("approxDurationMs")]
        public string ApproxDurationMs { get; set; }

        [JsonProperty("qualityOrdinal")]
        public string QualityOrdinal { get; set; }

        [JsonProperty("colorInfo")]
        public ColorInfo ColorInfo { get; set; }

        [JsonProperty("audioQuality")]
        public string AudioQuality { get; set; }

        [JsonProperty("audioSampleRate")]
        public string AudioSampleRate { get; set; }

        [JsonProperty("audioChannels")]
        public int? AudioChannels { get; set; }

        [JsonProperty("loudnessDb")]
        public double? LoudnessDb { get; set; }

        [JsonProperty("trackAbsoluteLoudnessLkfs")]
        public double? TrackAbsoluteLoudnessLkfs { get; set; }
    }

    public class AdRequestConfig
    {
        [JsonProperty("filterTimeEventsOnDelta")]
        public int FilterTimeEventsOnDelta { get; set; }

        [JsonProperty("useCriticalExecOnAdsPrep")]
        public bool UseCriticalExecOnAdsPrep { get; set; }

        [JsonProperty("userCriticalExecOnAdsProcessing")]
        public bool UserCriticalExecOnAdsProcessing { get; set; }

        [JsonProperty("enableCountdownNextToThumbnailAndroid")]
        public bool EnableCountdownNextToThumbnailAndroid { get; set; }

        [JsonProperty("preskipScalingFactorAndroid")]
        public double PreskipScalingFactorAndroid { get; set; }

        [JsonProperty("preskipPaddingAndroid")]
        public int PreskipPaddingAndroid { get; set; }
    }

    public class AdsContext
    {
        [JsonProperty("experimentFlags")]
        public ExperimentFlags ExperimentFlags { get; set; }
    }

    public class AdSurveyRequestConfig
    {
        [JsonProperty("useGetRequests")]
        public bool UseGetRequests { get; set; }
    }

    public class AndroidCronetResponsePriority
    {
        [JsonProperty("priorityValue")]
        public string PriorityValue { get; set; }
    }

    public class AndroidMedialibConfig
    {
        [JsonProperty("isItag18MainProfile")]
        public bool IsItag18MainProfile { get; set; }

        [JsonProperty("initialBandwidthEstimates")]
        public List<InitialBandwidthEstimate> InitialBandwidthEstimates { get; set; }

        [JsonProperty("viewportSizeFraction")]
        public double ViewportSizeFraction { get; set; }

        [JsonProperty("enablePrerollPrebuffer")]
        public bool EnablePrerollPrebuffer { get; set; }

        [JsonProperty("prebufferOptimizeForViewportSize")]
        public bool PrebufferOptimizeForViewportSize { get; set; }

        [JsonProperty("hpqViewportSizeFraction")]
        public double HpqViewportSizeFraction { get; set; }
    }

    public class AndroidMetadataNetworkConfig
    {
        [JsonProperty("coalesceRequests")]
        public bool CoalesceRequests { get; set; }
    }

    public class AndroidNetworkStackConfig
    {
        [JsonProperty("networkStack")]
        public string NetworkStack { get; set; }

        [JsonProperty("androidCronetResponsePriority")]
        public AndroidCronetResponsePriority AndroidCronetResponsePriority { get; set; }

        [JsonProperty("androidMetadataNetworkConfig")]
        public AndroidMetadataNetworkConfig AndroidMetadataNetworkConfig { get; set; }
    }

    public class AndroidPlayerStatsConfig
    {
        [JsonProperty("usePblForAttestationReporting")]
        public bool UsePblForAttestationReporting { get; set; }

        [JsonProperty("usePblForHeartbeatReporting")]
        public bool UsePblForHeartbeatReporting { get; set; }

        [JsonProperty("usePblForPlaybacktrackingReporting")]
        public bool UsePblForPlaybacktrackingReporting { get; set; }

        [JsonProperty("usePblForQoeReporting")]
        public bool UsePblForQoeReporting { get; set; }

        [JsonProperty("changeCpnOnFatalPlaybackError")]
        public bool ChangeCpnOnFatalPlaybackError { get; set; }
    }

    public class AnimationDecision
    {
        [JsonProperty("key")]
        public int Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class AtrUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("elapsedMediaTimeSeconds")]
        public int ElapsedMediaTimeSeconds { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class Attestation
    {
        [JsonProperty("playerAttestationRenderer")]
        public PlayerAttestationRenderer PlayerAttestationRenderer { get; set; }
    }

    public class AudioConfig
    {
        [JsonProperty("loudnessDb")]
        public double LoudnessDb { get; set; }

        [JsonProperty("perceptualLoudnessDb")]
        public double PerceptualLoudnessDb { get; set; }

        [JsonProperty("enablePerFormatLoudness")]
        public bool EnablePerFormatLoudness { get; set; }

        [JsonProperty("trackAbsoluteLoudnessLkfs")]
        public double TrackAbsoluteLoudnessLkfs { get; set; }

        [JsonProperty("loudnessTargetLkfs")]
        public int LoudnessTargetLkfs { get; set; }
    }

    public class AvailablePlaybackSpeed
    {
        [JsonProperty("label")]
        public Label Label { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class BandwidthEstimationConfig
    {
        [JsonProperty("nearestRankConfig")]
        public NearestRankConfig NearestRankConfig { get; set; }
    }

    public class BrowseEndpoint
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }
    }

    public class ButtonRenderer
    {
        [JsonProperty("serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class ButtonText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class CacheLoadPolicy
    {
        [JsonProperty("readaheadThresholdMs")]
        public int ReadaheadThresholdMs { get; set; }
    }

    public class CallToAction
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class CapabilitiesUpdate
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("capabilitiesBytes")]
        public string CapabilitiesBytes { get; set; }

        [JsonProperty("resourceTag")]
        public string ResourceTag { get; set; }
    }

    public class ChannelsContext
    {
        [JsonProperty("experimentFlags")]
        public ExperimentFlags ExperimentFlags { get; set; }
    }

    public class CmsPathProbeConfig
    {
        [JsonProperty("cmsPathProbeDelayMs")]
        public int CmsPathProbeDelayMs { get; set; }
    }

    public class ColorInfo
    {
        [JsonProperty("primaries")]
        public string Primaries { get; set; }

        [JsonProperty("transferCharacteristics")]
        public string TransferCharacteristics { get; set; }

        [JsonProperty("matrixCoefficients")]
        public string MatrixCoefficients { get; set; }
    }

    public class Command
    {
        [JsonProperty("innertubeCommand")]
        public InnertubeCommand InnertubeCommand { get; set; }
    }

    public class CommandMetadata
    {
        [JsonProperty("interactionLoggingCommandMetadata")]
        public InteractionLoggingCommandMetadata InteractionLoggingCommandMetadata { get; set; }
    }

    public class CommandWrapper
    {
        [JsonProperty("command")]
        public Command Command { get; set; }

        [JsonProperty("loggingDirectives")]
        public LoggingDirectives LoggingDirectives { get; set; }
    }

    public class CommonConfig
    {
    }

    public class ComponentType
    {
        [JsonProperty("templateConfig")]
        public TemplateConfig TemplateConfig { get; set; }

        [JsonProperty("model")]
        public Model Model { get; set; }

        [JsonProperty("subscriptionConfig")]
        public SubscriptionConfig SubscriptionConfig { get; set; }
    }

    public class Content
    {
        [JsonProperty("elementRenderer")]
        public ElementRenderer ElementRenderer { get; set; }
    }

    public class Context
    {
        [JsonProperty("staticDeviceEnvDataContext")]
        public StaticDeviceEnvDataContext StaticDeviceEnvDataContext { get; set; }

        [JsonProperty("typographyContext")]
        public TypographyContext TypographyContext { get; set; }

        [JsonProperty("subscriptionsContext")]
        public SubscriptionsContext SubscriptionsContext { get; set; }

        [JsonProperty("mainAppContext")]
        public MainAppContext MainAppContext { get; set; }

        [JsonProperty("reelsPlayerContext")]
        public ReelsPlayerContext ReelsPlayerContext { get; set; }

        [JsonProperty("themeKey")]
        public string ThemeKey { get; set; }

        [JsonProperty("shoppingAppContext")]
        public ShoppingAppContext ShoppingAppContext { get; set; }

        [JsonProperty("adsContext")]
        public AdsContext AdsContext { get; set; }

        [JsonProperty("channelsContext")]
        public ChannelsContext ChannelsContext { get; set; }

        [JsonProperty("musicContext")]
        public MusicContext MusicContext { get; set; }

        [JsonProperty("mainAppAdaptiveContext")]
        public MainAppAdaptiveContext MainAppAdaptiveContext { get; set; }

        [JsonProperty("clientCapabilitiesKey")]
        public string ClientCapabilitiesKey { get; set; }
    }

    public class Data
    {
        [JsonProperty("startTimeMs")]
        public string StartTimeMs { get; set; }

        [JsonProperty("endTimeMs")]
        public string EndTimeMs { get; set; }

        [JsonProperty("watermark")]
        public Watermark Watermark { get; set; }
    }

    public class DataSaverConfig
    {
        [JsonProperty("simpleBitrateCap")]
        public string SimpleBitrateCap { get; set; }
    }

    public class DataStoreSubscriptionConfig
    {
        [JsonProperty("mappings")]
        public List<Mapping> Mappings { get; set; }

        [JsonProperty("resultField")]
        public int ResultField { get; set; }
    }

    public class DecodeQualityConfig
    {
        [JsonProperty("maximumVideoDecodeVerticalResolution")]
        public int MaximumVideoDecodeVerticalResolution { get; set; }
    }

    public class Dismiss
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class DynamicReadaheadConfig
    {
        [JsonProperty("maxReadAheadMediaTimeMs")]
        public int MaxReadAheadMediaTimeMs { get; set; }

        [JsonProperty("minReadAheadMediaTimeMs")]
        public int MinReadAheadMediaTimeMs { get; set; }

        [JsonProperty("readAheadGrowthRateMs")]
        public int ReadAheadGrowthRateMs { get; set; }

        [JsonProperty("readAheadWatermarkMarginRatio")]
        public int ReadAheadWatermarkMarginRatio { get; set; }

        [JsonProperty("minReadAheadWatermarkMarginMs")]
        public int MinReadAheadWatermarkMarginMs { get; set; }

        [JsonProperty("maxReadAheadWatermarkMarginMs")]
        public int MaxReadAheadWatermarkMarginMs { get; set; }

        [JsonProperty("shouldIncorporateNetworkActiveState")]
        public bool ShouldIncorporateNetworkActiveState { get; set; }
    }

    public class Element
    {
        [JsonProperty("endscreenElementRenderer")]
        public EndscreenElementRenderer EndscreenElementRenderer { get; set; }
    }

    public class ElementRenderer
    {
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("newElement")]
        public NewElement NewElement { get; set; }
    }

    public class ElementUpdate
    {
        [JsonProperty("updates")]
        public List<Update> Updates { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Endpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }

        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }

        [JsonProperty("browseEndpoint")]
        public BrowseEndpoint BrowseEndpoint { get; set; }
    }

    public class Endscreen
    {
        [JsonProperty("endscreenRenderer")]
        public EndscreenRenderer EndscreenRenderer { get; set; }
    }

    public class EndscreenElementRenderer
    {
        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("top")]
        public double Top { get; set; }

        [JsonProperty("aspectRatio")]
        public double AspectRatio { get; set; }

        [JsonProperty("startMs")]
        public string StartMs { get; set; }

        [JsonProperty("endMs")]
        public string EndMs { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }

        [JsonProperty("playlistLength")]
        public PlaylistLength PlaylistLength { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("callToAction")]
        public CallToAction CallToAction { get; set; }

        [JsonProperty("dismiss")]
        public Dismiss Dismiss { get; set; }

        [JsonProperty("hovercardButton")]
        public HovercardButton HovercardButton { get; set; }

        [JsonProperty("isSubscribe")]
        public bool? IsSubscribe { get; set; }
    }

    public class EndscreenRenderer
    {
        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        [JsonProperty("startMs")]
        public string StartMs { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class EngageUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class EntityBatchUpdate
    {
        [JsonProperty("mutations")]
        public List<Mutation> Mutations { get; set; }

        [JsonProperty("timestamp")]
        public Timestamp Timestamp { get; set; }
    }

    public class Environment
    {
        [JsonProperty("platformName")]
        public string PlatformName { get; set; }
    }

    public class EnvironmentSubscriptionConfig
    {
        [JsonProperty("resultField")]
        public int ResultField { get; set; }

        [JsonProperty("environmentDataField")]
        public int EnvironmentDataField { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }
    }

    public class EomFlowRenderer
    {
        [JsonProperty("webViewRenderer")]
        public WebViewRenderer WebViewRenderer { get; set; }
    }

    public class ExoPlayerConfig
    {
        [JsonProperty("useExoPlayer")]
        public bool UseExoPlayer { get; set; }

        [JsonProperty("useAdaptiveBitrate")]
        public bool UseAdaptiveBitrate { get; set; }

        [JsonProperty("maxInitialByteRate")]
        public int MaxInitialByteRate { get; set; }

        [JsonProperty("minDurationForQualityIncreaseMs")]
        public int MinDurationForQualityIncreaseMs { get; set; }

        [JsonProperty("maxDurationForQualityDecreaseMs")]
        public int MaxDurationForQualityDecreaseMs { get; set; }

        [JsonProperty("minDurationToRetainAfterDiscardMs")]
        public int MinDurationToRetainAfterDiscardMs { get; set; }

        [JsonProperty("lowWatermarkMs")]
        public int LowWatermarkMs { get; set; }

        [JsonProperty("highWatermarkMs")]
        public int HighWatermarkMs { get; set; }

        [JsonProperty("lowPoolLoad")]
        public double LowPoolLoad { get; set; }

        [JsonProperty("highPoolLoad")]
        public double HighPoolLoad { get; set; }

        [JsonProperty("sufficientBandwidthOverhead")]
        public double SufficientBandwidthOverhead { get; set; }

        [JsonProperty("bufferChunkSizeKb")]
        public int BufferChunkSizeKb { get; set; }

        [JsonProperty("httpConnectTimeoutMs")]
        public int HttpConnectTimeoutMs { get; set; }

        [JsonProperty("httpReadTimeoutMs")]
        public int HttpReadTimeoutMs { get; set; }

        [JsonProperty("numAudioSegmentsPerFetch")]
        public int NumAudioSegmentsPerFetch { get; set; }

        [JsonProperty("numVideoSegmentsPerFetch")]
        public int NumVideoSegmentsPerFetch { get; set; }

        [JsonProperty("minDurationForPlaybackStartMs")]
        public int MinDurationForPlaybackStartMs { get; set; }

        [JsonProperty("enableExoplayerReuse")]
        public bool EnableExoplayerReuse { get; set; }

        [JsonProperty("useRadioTypeForInitialQualitySelection")]
        public bool UseRadioTypeForInitialQualitySelection { get; set; }

        [JsonProperty("blacklistFormatOnError")]
        public bool BlacklistFormatOnError { get; set; }

        [JsonProperty("enableBandaidHttpDataSource")]
        public bool EnableBandaidHttpDataSource { get; set; }

        [JsonProperty("httpLoadTimeoutMs")]
        public int HttpLoadTimeoutMs { get; set; }

        [JsonProperty("canPlayHdDrm")]
        public bool CanPlayHdDrm { get; set; }

        [JsonProperty("videoBufferSegmentCount")]
        public int VideoBufferSegmentCount { get; set; }

        [JsonProperty("audioBufferSegmentCount")]
        public int AudioBufferSegmentCount { get; set; }

        [JsonProperty("useAbruptSplicing")]
        public bool UseAbruptSplicing { get; set; }

        [JsonProperty("minRetryCount")]
        public int MinRetryCount { get; set; }

        [JsonProperty("minChunksNeededToPreferOffline")]
        public int MinChunksNeededToPreferOffline { get; set; }

        [JsonProperty("secondsToMaxAggressiveness")]
        public int SecondsToMaxAggressiveness { get; set; }

        [JsonProperty("enableSurfaceviewResizeWorkaround")]
        public bool EnableSurfaceviewResizeWorkaround { get; set; }

        [JsonProperty("enableVp9IfThresholdsPass")]
        public bool EnableVp9IfThresholdsPass { get; set; }

        [JsonProperty("matchQualityToViewportOnUnfullscreen")]
        public bool MatchQualityToViewportOnUnfullscreen { get; set; }

        [JsonProperty("lowAudioQualityConnTypes")]
        public List<string> LowAudioQualityConnTypes { get; set; }

        [JsonProperty("useDashForLiveStreams")]
        public bool UseDashForLiveStreams { get; set; }

        [JsonProperty("enableLibvpxVideoTrackRenderer")]
        public bool EnableLibvpxVideoTrackRenderer { get; set; }

        [JsonProperty("lowAudioQualityBandwidthThresholdBps")]
        public int LowAudioQualityBandwidthThresholdBps { get; set; }

        [JsonProperty("enableVariableSpeedPlayback")]
        public bool EnableVariableSpeedPlayback { get; set; }

        [JsonProperty("preferOnesieBufferedFormat")]
        public bool PreferOnesieBufferedFormat { get; set; }

        [JsonProperty("minimumBandwidthSampleBytes")]
        public int MinimumBandwidthSampleBytes { get; set; }

        [JsonProperty("useDashForOtfAndCompletedLiveStreams")]
        public bool UseDashForOtfAndCompletedLiveStreams { get; set; }

        [JsonProperty("disableCacheAwareVideoFormatEvaluation")]
        public bool DisableCacheAwareVideoFormatEvaluation { get; set; }

        [JsonProperty("useLiveDvrForDashLiveStreams")]
        public bool UseLiveDvrForDashLiveStreams { get; set; }

        [JsonProperty("cronetResetTimeoutOnRedirects")]
        public bool CronetResetTimeoutOnRedirects { get; set; }

        [JsonProperty("emitVideoDecoderChangeEvents")]
        public bool EmitVideoDecoderChangeEvents { get; set; }

        [JsonProperty("onesieVideoBufferLoadTimeoutMs")]
        public string OnesieVideoBufferLoadTimeoutMs { get; set; }

        [JsonProperty("onesieVideoBufferReadTimeoutMs")]
        public string OnesieVideoBufferReadTimeoutMs { get; set; }

        [JsonProperty("libvpxEnableGl")]
        public bool LibvpxEnableGl { get; set; }

        [JsonProperty("enableVp9EncryptedIfThresholdsPass")]
        public bool EnableVp9EncryptedIfThresholdsPass { get; set; }

        [JsonProperty("enableOpus")]
        public bool EnableOpus { get; set; }

        [JsonProperty("usePredictedBuffer")]
        public bool UsePredictedBuffer { get; set; }

        [JsonProperty("maxReadAheadMediaTimeMs")]
        public int MaxReadAheadMediaTimeMs { get; set; }

        [JsonProperty("useMediaTimeCappedLoadControl")]
        public bool UseMediaTimeCappedLoadControl { get; set; }

        [JsonProperty("allowCacheOverrideToLowerQualitiesWithinRange")]
        public int AllowCacheOverrideToLowerQualitiesWithinRange { get; set; }

        [JsonProperty("allowDroppingUndecodedFrames")]
        public bool AllowDroppingUndecodedFrames { get; set; }

        [JsonProperty("minDurationForPlaybackRestartMs")]
        public int MinDurationForPlaybackRestartMs { get; set; }

        [JsonProperty("serverProvidedBandwidthHeader")]
        public string ServerProvidedBandwidthHeader { get; set; }

        [JsonProperty("liveOnlyPegStrategy")]
        public string LiveOnlyPegStrategy { get; set; }

        [JsonProperty("enableRedirectorHostFallback")]
        public bool EnableRedirectorHostFallback { get; set; }

        [JsonProperty("enableHighlyAvailableFormatFallbackOnPcr")]
        public bool EnableHighlyAvailableFormatFallbackOnPcr { get; set; }

        [JsonProperty("recordTrackRendererTimingEvents")]
        public bool RecordTrackRendererTimingEvents { get; set; }

        [JsonProperty("minErrorsForRedirectorHostFallback")]
        public int MinErrorsForRedirectorHostFallback { get; set; }

        [JsonProperty("nonHardwareMediaCodecNames")]
        public List<string> NonHardwareMediaCodecNames { get; set; }

        [JsonProperty("enableVp9IfInHardware")]
        public bool EnableVp9IfInHardware { get; set; }

        [JsonProperty("enableVp9EncryptedIfInHardware")]
        public bool EnableVp9EncryptedIfInHardware { get; set; }

        [JsonProperty("useOpusMedAsLowQualityAudio")]
        public bool UseOpusMedAsLowQualityAudio { get; set; }

        [JsonProperty("minErrorsForPcrFallback")]
        public int MinErrorsForPcrFallback { get; set; }

        [JsonProperty("useStickyRedirectHttpDataSource")]
        public bool UseStickyRedirectHttpDataSource { get; set; }

        [JsonProperty("onlyVideoBandwidth")]
        public bool OnlyVideoBandwidth { get; set; }

        [JsonProperty("useRedirectorOnNetworkChange")]
        public bool UseRedirectorOnNetworkChange { get; set; }

        [JsonProperty("enableMaxReadaheadAbrThreshold")]
        public bool EnableMaxReadaheadAbrThreshold { get; set; }

        [JsonProperty("cacheCheckDirectoryWritabilityOnce")]
        public bool CacheCheckDirectoryWritabilityOnce { get; set; }

        [JsonProperty("predictorType")]
        public string PredictorType { get; set; }

        [JsonProperty("slidingPercentile")]
        public double SlidingPercentile { get; set; }

        [JsonProperty("slidingWindowSize")]
        public int SlidingWindowSize { get; set; }

        [JsonProperty("maxFrameDropIntervalMs")]
        public int MaxFrameDropIntervalMs { get; set; }

        [JsonProperty("ignoreLoadTimeoutForFallback")]
        public bool IgnoreLoadTimeoutForFallback { get; set; }

        [JsonProperty("serverBweMultiplier")]
        public int ServerBweMultiplier { get; set; }

        [JsonProperty("drmMaxKeyfetchDelayMs")]
        public int DrmMaxKeyfetchDelayMs { get; set; }

        [JsonProperty("maxResolutionForWhiteNoise")]
        public int MaxResolutionForWhiteNoise { get; set; }

        [JsonProperty("whiteNoiseRenderEffectMode")]
        public string WhiteNoiseRenderEffectMode { get; set; }

        [JsonProperty("enableLibvpxHdr")]
        public bool EnableLibvpxHdr { get; set; }

        [JsonProperty("enableCacheAwareStreamSelection")]
        public bool EnableCacheAwareStreamSelection { get; set; }

        [JsonProperty("useExoCronetDataSource")]
        public bool UseExoCronetDataSource { get; set; }

        [JsonProperty("whiteNoiseScale")]
        public int WhiteNoiseScale { get; set; }

        [JsonProperty("whiteNoiseOffset")]
        public int WhiteNoiseOffset { get; set; }

        [JsonProperty("preventVideoFrameLaggingWithLibvpx")]
        public bool PreventVideoFrameLaggingWithLibvpx { get; set; }

        [JsonProperty("enableMediaCodecHdr")]
        public bool EnableMediaCodecHdr { get; set; }

        [JsonProperty("enableMediaCodecSwHdr")]
        public bool EnableMediaCodecSwHdr { get; set; }

        [JsonProperty("liveOnlyWindowChunks")]
        public int LiveOnlyWindowChunks { get; set; }

        [JsonProperty("bearerMinDurationToRetainAfterDiscardMs")]
        public List<int> BearerMinDurationToRetainAfterDiscardMs { get; set; }

        [JsonProperty("forceWidevineL3")]
        public bool ForceWidevineL3 { get; set; }

        [JsonProperty("useAverageBitrate")]
        public bool UseAverageBitrate { get; set; }

        [JsonProperty("useMedialibAudioTrackRendererForLive")]
        public bool UseMedialibAudioTrackRendererForLive { get; set; }

        [JsonProperty("useExoPlayerV2")]
        public bool UseExoPlayerV2 { get; set; }

        [JsonProperty("logMediaRequestEventsToCsi")]
        public bool LogMediaRequestEventsToCsi { get; set; }

        [JsonProperty("onesieFixNonZeroStartTimeFormatSelection")]
        public bool OnesieFixNonZeroStartTimeFormatSelection { get; set; }

        [JsonProperty("liveOnlyReadaheadStepSizeChunks")]
        public int LiveOnlyReadaheadStepSizeChunks { get; set; }

        [JsonProperty("liveOnlyBufferHealthHalfLifeSeconds")]
        public int LiveOnlyBufferHealthHalfLifeSeconds { get; set; }

        [JsonProperty("liveOnlyMinBufferHealthRatio")]
        public double LiveOnlyMinBufferHealthRatio { get; set; }

        [JsonProperty("liveOnlyMinLatencyToSeekRatio")]
        public int LiveOnlyMinLatencyToSeekRatio { get; set; }

        [JsonProperty("manifestlessPartialChunkStrategy")]
        public string ManifestlessPartialChunkStrategy { get; set; }

        [JsonProperty("ignoreViewportSizeWhenSticky")]
        public bool IgnoreViewportSizeWhenSticky { get; set; }

        [JsonProperty("enableLibvpxFallback")]
        public bool EnableLibvpxFallback { get; set; }

        [JsonProperty("disableLibvpxLoopFilter")]
        public bool DisableLibvpxLoopFilter { get; set; }

        [JsonProperty("enableVpxMediaView")]
        public bool EnableVpxMediaView { get; set; }

        [JsonProperty("hdrMinScreenBrightness")]
        public int HdrMinScreenBrightness { get; set; }

        [JsonProperty("hdrMaxScreenBrightnessThreshold")]
        public int HdrMaxScreenBrightnessThreshold { get; set; }

        [JsonProperty("onesieDataSourceAboveCacheDataSource")]
        public bool OnesieDataSourceAboveCacheDataSource { get; set; }

        [JsonProperty("httpNonplayerLoadTimeoutMs")]
        public int HttpNonplayerLoadTimeoutMs { get; set; }

        [JsonProperty("numVideoSegmentsPerFetchStrategy")]
        public string NumVideoSegmentsPerFetchStrategy { get; set; }

        [JsonProperty("maxVideoDurationPerFetchMs")]
        public int MaxVideoDurationPerFetchMs { get; set; }

        [JsonProperty("maxVideoEstimatedLoadDurationMs")]
        public int MaxVideoEstimatedLoadDurationMs { get; set; }

        [JsonProperty("estimatedServerClockHalfLife")]
        public int EstimatedServerClockHalfLife { get; set; }

        [JsonProperty("estimatedServerClockStrictOffset")]
        public bool EstimatedServerClockStrictOffset { get; set; }

        [JsonProperty("minReadAheadMediaTimeMs")]
        public int MinReadAheadMediaTimeMs { get; set; }

        [JsonProperty("readAheadGrowthRate")]
        public int ReadAheadGrowthRate { get; set; }

        [JsonProperty("useDynamicReadAhead")]
        public bool UseDynamicReadAhead { get; set; }

        [JsonProperty("useYtVodMediaSourceForV2")]
        public bool UseYtVodMediaSourceForV2 { get; set; }

        [JsonProperty("enableV2Gapless")]
        public bool EnableV2Gapless { get; set; }

        [JsonProperty("useLiveHeadTimeMillis")]
        public bool UseLiveHeadTimeMillis { get; set; }

        [JsonProperty("allowTrackSelectionWithUpdatedVideoItagsForExoV2")]
        public bool AllowTrackSelectionWithUpdatedVideoItagsForExoV2 { get; set; }

        [JsonProperty("maxAllowableTimeBeforeMediaTimeUpdateSec")]
        public int MaxAllowableTimeBeforeMediaTimeUpdateSec { get; set; }

        [JsonProperty("enableDynamicHdr")]
        public bool EnableDynamicHdr { get; set; }

        [JsonProperty("v2PerformEarlyStreamSelection")]
        public bool V2PerformEarlyStreamSelection { get; set; }

        [JsonProperty("v2UsePlaybackStreamSelectionResult")]
        public bool V2UsePlaybackStreamSelectionResult { get; set; }

        [JsonProperty("v2MinTimeBetweenAbrReevaluationMs")]
        public int V2MinTimeBetweenAbrReevaluationMs { get; set; }

        [JsonProperty("avoidReusePlaybackAcrossLoadvideos")]
        public bool AvoidReusePlaybackAcrossLoadvideos { get; set; }

        [JsonProperty("enableInfiniteNetworkLoadingRetries")]
        public bool EnableInfiniteNetworkLoadingRetries { get; set; }

        [JsonProperty("reportExoPlayerStateOnTransition")]
        public bool ReportExoPlayerStateOnTransition { get; set; }

        [JsonProperty("manifestlessSequenceMethod")]
        public string ManifestlessSequenceMethod { get; set; }

        [JsonProperty("useLiveHeadWindow")]
        public bool UseLiveHeadWindow { get; set; }

        [JsonProperty("enableDynamicHdrInHardware")]
        public bool EnableDynamicHdrInHardware { get; set; }

        [JsonProperty("ultralowAudioQualityBandwidthThresholdBps")]
        public int UltralowAudioQualityBandwidthThresholdBps { get; set; }

        [JsonProperty("retryLiveNetNocontentWithDelay")]
        public bool RetryLiveNetNocontentWithDelay { get; set; }

        [JsonProperty("ignoreUnneededSeeksToLiveHead")]
        public bool IgnoreUnneededSeeksToLiveHead { get; set; }

        [JsonProperty("adaptiveLiveHeadWindow")]
        public bool AdaptiveLiveHeadWindow { get; set; }

        [JsonProperty("drmMetricsQoeLoggingFraction")]
        public double DrmMetricsQoeLoggingFraction { get; set; }

        [JsonProperty("liveNetNocontentMaximumErrors")]
        public int LiveNetNocontentMaximumErrors { get; set; }

        [JsonProperty("waitForDrmLicenseBeforeProcessingAndroidStuckBufferfull")]
        public bool WaitForDrmLicenseBeforeProcessingAndroidStuckBufferfull { get; set; }

        [JsonProperty("slidingPercentileScalar")]
        public double SlidingPercentileScalar { get; set; }

        [JsonProperty("minAdaptiveVideoQuality")]
        public int MinAdaptiveVideoQuality { get; set; }

        [JsonProperty("retryLiveEmptyChunkWithDelay")]
        public bool RetryLiveEmptyChunkWithDelay { get; set; }

        [JsonProperty("platypusBackBufferDurationMs")]
        public int PlatypusBackBufferDurationMs { get; set; }

        [JsonProperty("platypusEnableServerSideFormatFiltering")]
        public bool PlatypusEnableServerSideFormatFiltering { get; set; }
    }

    public class ExoPlayerInitConfig
    {
        [JsonProperty("exoPlayerConfig")]
        public ExoPlayerConfig ExoPlayerConfig { get; set; }
    }

    public class ExpectedParentScreen
    {
        [JsonProperty("screenVeType")]
        public int ScreenVeType { get; set; }
    }

    public class ExperimentFlags
    {
        [JsonProperty("enableRhsPanelOnElements")]
        public bool EnableRhsPanelOnElements { get; set; }

        [JsonProperty("enableEngagementHeaderA11yFix")]
        public bool EnableEngagementHeaderA11yFix { get; set; }

        [JsonProperty("showCarouselOnDwellForVacTravelInfeedDwellDurationMs")]
        public int ShowCarouselOnDwellForVacTravelInfeedDwellDurationMs { get; set; }

        [JsonProperty("enableAdsLandscapeEngagementPanelOnIosServer")]
        public bool EnableAdsLandscapeEngagementPanelOnIosServer { get; set; }

        [JsonProperty("enableFlattenRhsPanelOnElementsForShortsAds")]
        public bool EnableFlattenRhsPanelOnElementsForShortsAds { get; set; }

        [JsonProperty("transitionDurationAdSwappingInSec")]
        public double TransitionDurationAdSwappingInSec { get; set; }

        [JsonProperty("useCellForStoreVisitPanel")]
        public bool UseCellForStoreVisitPanel { get; set; }

        [JsonProperty("delayedCarouselExtensionScale")]
        public int DelayedCarouselExtensionScale { get; set; }

        [JsonProperty("enableSquareImageLayoutA11yFix")]
        public bool EnableSquareImageLayoutA11yFix { get; set; }

        [JsonProperty("enableCapabilityOnFlattenedRhsForShortsAds")]
        public bool EnableCapabilityOnFlattenedRhsForShortsAds { get; set; }

        [JsonProperty("shortsAdLeaveBehindSecondsToInvisible")]
        public double ShortsAdLeaveBehindSecondsToInvisible { get; set; }

        [JsonProperty("shortsNavigationFadeInDelaySeconds")]
        public double ShortsNavigationFadeInDelaySeconds { get; set; }

        [JsonProperty("shortsNavigationAnimationDurationSeconds")]
        public double ShortsNavigationAnimationDurationSeconds { get; set; }

        [JsonProperty("shortsAdLeaveBehindSecondsToAutoDismiss")]
        public int ShortsAdLeaveBehindSecondsToAutoDismiss { get; set; }

        [JsonProperty("enableLidaIosFix")]
        public bool EnableLidaIosFix { get; set; }

        [JsonProperty("enableSmallerClickAreaShortsImageAdTooltip")]
        public bool EnableSmallerClickAreaShortsImageAdTooltip { get; set; }

        [JsonProperty("enableShortsCompactSurvey")]
        public bool EnableShortsCompactSurvey { get; set; }

        [JsonProperty("shortsCompactSurveyAnimationOutDurationSeconds")]
        public double ShortsCompactSurveyAnimationOutDurationSeconds { get; set; }

        [JsonProperty("websiteAutoClickCountdownSeconds")]
        public string WebsiteAutoClickCountdownSeconds { get; set; }

        [JsonProperty("disableShortsImageA11yFocus")]
        public bool DisableShortsImageA11yFocus { get; set; }

        [JsonProperty("shiftCtaOverlaySixteenPxFromBottomForShortsAds")]
        public bool ShiftCtaOverlaySixteenPxFromBottomForShortsAds { get; set; }

        [JsonProperty("disableShortsNavigationSelectedBorder")]
        public bool DisableShortsNavigationSelectedBorder { get; set; }

        [JsonProperty("enableVerticalPaddingForEngagementFooter")]
        public bool EnableVerticalPaddingForEngagementFooter { get; set; }

        [JsonProperty("enableProductFeedBadgeOnTapCommand")]
        public bool EnableProductFeedBadgeOnTapCommand { get; set; }

        [JsonProperty("enableProductFeedCheckoutIosA11yFix")]
        public bool EnableProductFeedCheckoutIosA11yFix { get; set; }

        [JsonProperty("enableShortsUxFixForVideoAds")]
        public bool EnableShortsUxFixForVideoAds { get; set; }

        [JsonProperty("enableAdEngagementPanelBottomAnimation")]
        public bool EnableAdEngagementPanelBottomAnimation { get; set; }

        [JsonProperty("adSwappingDismissAdButtonTitle")]
        public string AdSwappingDismissAdButtonTitle { get; set; }

        [JsonProperty("enableStoreInsetValuesForTopCarousel")]
        public bool EnableStoreInsetValuesForTopCarousel { get; set; }

        [JsonProperty("disableTopCarouselScrollingShortsNavigation")]
        public bool DisableTopCarouselScrollingShortsNavigation { get; set; }

        [JsonProperty("enableIntersectionObserverForProgresssiveDisclosure")]
        public bool EnableIntersectionObserverForProgresssiveDisclosure { get; set; }

        [JsonProperty("enableCarouselLhsFix")]
        public bool EnableCarouselLhsFix { get; set; }

        [JsonProperty("swapOpenInNewWithArrowDiagonalUpRightOnMobile")]
        public bool SwapOpenInNewWithArrowDiagonalUpRightOnMobile { get; set; }

        [JsonProperty("enableFixDoublePaddingForDelayedCta")]
        public bool EnableFixDoublePaddingForDelayedCta { get; set; }

        [JsonProperty("enableAdEngagementPanelIconUpdate")]
        public bool EnableAdEngagementPanelIconUpdate { get; set; }

        [JsonProperty("autoTriggerAdImageTooltipDelayMs")]
        public int AutoTriggerAdImageTooltipDelayMs { get; set; }

        [JsonProperty("enableFixAdBadgeSponsoredA11y")]
        public bool EnableFixAdBadgeSponsoredA11y { get; set; }

        [JsonProperty("populateThemeServingModeForImageAds")]
        public bool PopulateThemeServingModeForImageAds { get; set; }

        [JsonProperty("enableProgressiveDisclosureImageAds")]
        public bool EnableProgressiveDisclosureImageAds { get; set; }

        [JsonProperty("feedOverlayDisableCtaBeforeDismiss")]
        public bool FeedOverlayDisableCtaBeforeDismiss { get; set; }

        [JsonProperty("enableInfeedSkoverlayRendering")]
        public bool EnableInfeedSkoverlayRendering { get; set; }

        [JsonProperty("largeTextFontSizeOnShortsAds")]
        public int LargeTextFontSizeOnShortsAds { get; set; }

        [JsonProperty("largeCtaFontSizeOnShortsAds")]
        public int LargeCtaFontSizeOnShortsAds { get; set; }

        [JsonProperty("largeCtaOverlayFontSizeOnShortsAds")]
        public int LargeCtaOverlayFontSizeOnShortsAds { get; set; }

        [JsonProperty("supportRtlSettingsForShortsAds")]
        public bool SupportRtlSettingsForShortsAds { get; set; }

        [JsonProperty("enableAddTopPaddingForTopCarousel")]
        public bool EnableAddTopPaddingForTopCarousel { get; set; }

        [JsonProperty("dynamicCardHeightOnShortsAds")]
        public int DynamicCardHeightOnShortsAds { get; set; }

        [JsonProperty("dynamicCardWidthOnShortsAds")]
        public int DynamicCardWidthOnShortsAds { get; set; }

        [JsonProperty("expPanelButtonCenteredCtaNudgeSeconds")]
        public int ExpPanelButtonCenteredCtaNudgeSeconds { get; set; }

        [JsonProperty("enablePauseAdsImageFit")]
        public bool EnablePauseAdsImageFit { get; set; }

        [JsonProperty("enableUxPolishFixForAdTextBox")]
        public bool EnableUxPolishFixForAdTextBox { get; set; }

        [JsonProperty("setOverflowButtonEndMarginToZeroForWatchNextFullscreenLandscape")]
        public bool SetOverflowButtonEndMarginToZeroForWatchNextFullscreenLandscape { get; set; }

        [JsonProperty("enableFixDoublePaddingForImageAds")]
        public bool EnableFixDoublePaddingForImageAds { get; set; }

        [JsonProperty("fixSubscribeButtonForWatchNextFullscreenLandscape")]
        public bool FixSubscribeButtonForWatchNextFullscreenLandscape { get; set; }

        [JsonProperty("fixThumbnailSizeForWatchNextFullscreenLandscape")]
        public bool FixThumbnailSizeForWatchNextFullscreenLandscape { get; set; }

        [JsonProperty("fixRightMarginForRve")]
        public bool FixRightMarginForRve { get; set; }

        [JsonProperty("expLppProgressiveDisclosureAvatarShrinkTime")]
        public int ExpLppProgressiveDisclosureAvatarShrinkTime { get; set; }

        [JsonProperty("expLppProgressiveDisclosureImageZoomTime")]
        public int ExpLppProgressiveDisclosureImageZoomTime { get; set; }

        [JsonProperty("expLppProgressiveDisclosureSwipeUpTime")]
        public int ExpLppProgressiveDisclosureSwipeUpTime { get; set; }

        [JsonProperty("expAppAdsProgressiveDisclosureImageZoomTime")]
        public int ExpAppAdsProgressiveDisclosureImageZoomTime { get; set; }

        [JsonProperty("expAppAdsProgressiveDisclosureBkgGradientTime")]
        public int ExpAppAdsProgressiveDisclosureBkgGradientTime { get; set; }

        [JsonProperty("fixA11yUnlabeledReadoutForAdDisclosureBanner")]
        public bool FixA11yUnlabeledReadoutForAdDisclosureBanner { get; set; }

        [JsonProperty("enableReelsAdCardForCreatorPromoFormat")]
        public bool EnableReelsAdCardForCreatorPromoFormat { get; set; }

        [JsonProperty("enableFixOverlayIconForWatchNextFullscreen")]
        public bool EnableFixOverlayIconForWatchNextFullscreen { get; set; }

        [JsonProperty("enableMyAdCenterHeaderAccessibilityContainer")]
        public bool EnableMyAdCenterHeaderAccessibilityContainer { get; set; }

        [JsonProperty("expShoppingProgressiveDisclosureAvatarShrinkTime")]
        public int ExpShoppingProgressiveDisclosureAvatarShrinkTime { get; set; }

        [JsonProperty("expShoppingProgressiveDisclosureImageZoomTime")]
        public int ExpShoppingProgressiveDisclosureImageZoomTime { get; set; }

        [JsonProperty("enableAdIconTextMinWidthFix")]
        public bool EnableAdIconTextMinWidthFix { get; set; }

        [JsonProperty("fixThumbnailSizeForImageAdsOnWatchNextFullscreenLandscape")]
        public bool FixThumbnailSizeForImageAdsOnWatchNextFullscreenLandscape { get; set; }

        [JsonProperty("constrainThumbnailDisclosureBannerForAndroidHomeLandscape")]
        public bool ConstrainThumbnailDisclosureBannerForAndroidHomeLandscape { get; set; }

        [JsonProperty("enableOverlayButtonForTextImage")]
        public bool EnableOverlayButtonForTextImage { get; set; }

        [JsonProperty("expAdImageKenBurnsAnimationType")]
        public string ExpAdImageKenBurnsAnimationType { get; set; }

        [JsonProperty("enableShortsImageButtonTransitionFix")]
        public bool EnableShortsImageButtonTransitionFix { get; set; }

        [JsonProperty("enableResponsiveAdsOnTabletHome")]
        public bool EnableResponsiveAdsOnTabletHome { get; set; }

        [JsonProperty("removeChevronFromAdDisclosureBannerEml")]
        public bool RemoveChevronFromAdDisclosureBannerEml { get; set; }

        [JsonProperty("expAppAdsProgressiveDisclosureHeaderShrinkTime")]
        public int ExpAppAdsProgressiveDisclosureHeaderShrinkTime { get; set; }

        [JsonProperty("enableAdDisclosureBannerOnPlayerOverlay")]
        public bool EnableAdDisclosureBannerOnPlayerOverlay { get; set; }

        [JsonProperty("enableViewPronounsOnMainApp")]
        public bool EnableViewPronounsOnMainApp { get; set; }
    }

    public class Experiments
    {
        [JsonProperty("forceModernSubscribeButton")]
        public bool ForceModernSubscribeButton { get; set; }

        [JsonProperty("enableModernButtons")]
        public bool EnableModernButtons { get; set; }

        [JsonProperty("enableRoundedThumbs")]
        public bool EnableRoundedThumbs { get; set; }

        [JsonProperty("useDarkerPaletteBgColorForServer")]
        public bool UseDarkerPaletteBgColorForServer { get; set; }

        [JsonProperty("enableModernChipsV1")]
        public bool EnableModernChipsV1 { get; set; }

        [JsonProperty("bannerTextIconDarkThemeBackgroundColor")]
        public string BannerTextIconDarkThemeBackgroundColor { get; set; }

        [JsonProperty("enableBlocksErrorRecoveryForElementsPlaylistPicker")]
        public bool EnableBlocksErrorRecoveryForElementsPlaylistPicker { get; set; }

        [JsonProperty("enableCinematicContainer")]
        public bool EnableCinematicContainer { get; set; }

        [JsonProperty("useAmsterdamColorsForServerLongTail")]
        public bool UseAmsterdamColorsForServerLongTail { get; set; }

        [JsonProperty("enableAmsterdamFitAndFinish")]
        public bool EnableAmsterdamFitAndFinish { get; set; }

        [JsonProperty("enableRoundedBadgesForServer")]
        public bool EnableRoundedBadgesForServer { get; set; }

        [JsonProperty("enableModernSnackbar")]
        public bool EnableModernSnackbar { get; set; }

        [JsonProperty("enableControllerEagerInit")]
        public bool EnableControllerEagerInit { get; set; }

        [JsonProperty("enableCreatorDetailsShelfM2")]
        public bool EnableCreatorDetailsShelfM2 { get; set; }

        [JsonProperty("enableModernTypeComponentsServer")]
        public bool EnableModernTypeComponentsServer { get; set; }

        [JsonProperty("enableDescriptionVerticalFadedScrim")]
        public bool EnableDescriptionVerticalFadedScrim { get; set; }

        [JsonProperty("ycEnableUnifiedChannelCreationFlow")]
        public bool YcEnableUnifiedChannelCreationFlow { get; set; }

        [JsonProperty("enableModernTypePostBaServer")]
        public bool EnableModernTypePostBaServer { get; set; }

        [JsonProperty("enableLearningConceptContextualDefinitions")]
        public bool EnableLearningConceptContextualDefinitions { get; set; }

        [JsonProperty("textFieldSupportedForChannelEditing")]
        public bool TextFieldSupportedForChannelEditing { get; set; }

        [JsonProperty("enableBlockDataSourceInitialElements")]
        public bool EnableBlockDataSourceInitialElements { get; set; }

        [JsonProperty("enableLearningConceptGfeedback")]
        public bool EnableLearningConceptGfeedback { get; set; }

        [JsonProperty("collectionThumbnailTransitionDurationSec")]
        public double CollectionThumbnailTransitionDurationSec { get; set; }

        [JsonProperty("collectionTextTransitionDurationSec")]
        public double CollectionTextTransitionDurationSec { get; set; }

        [JsonProperty("collectionLoopDelayDurationSec")]
        public double CollectionLoopDelayDurationSec { get; set; }

        [JsonProperty("enableBookmarkSaveIcon")]
        public bool EnableBookmarkSaveIcon { get; set; }

        [JsonProperty("scrollIntoVisibleAreaInBottomSheets")]
        public bool ScrollIntoVisibleAreaInBottomSheets { get; set; }

        [JsonProperty("fixAndroidMobileHistoryShelfLoadJank")]
        public bool FixAndroidMobileHistoryShelfLoadJank { get; set; }

        [JsonProperty("enableModernizeStructuredDescriptionPlaylistLockupsV2")]
        public bool EnableModernizeStructuredDescriptionPlaylistLockupsV2 { get; set; }

        [JsonProperty("emlPerfTest")]
        public int EmlPerfTest { get; set; }

        [JsonProperty("disableTouchFeedbackOnVideoCardsWithoutOnTapCommand")]
        public bool DisableTouchFeedbackOnVideoCardsWithoutOnTapCommand { get; set; }

        [JsonProperty("enableResponsiveAvatarRowBreakpoint")]
        public int EnableResponsiveAvatarRowBreakpoint { get; set; }

        [JsonProperty("enableContentsAsElementViewModelForHorizontalShelf")]
        public bool EnableContentsAsElementViewModelForHorizontalShelf { get; set; }

        [JsonProperty("enableVideoLockupMetadataServerFilling")]
        public bool EnableVideoLockupMetadataServerFilling { get; set; }

        [JsonProperty("fixViewAllButtonAlignmentInHorizontalShelf")]
        public bool FixViewAllButtonAlignmentInHorizontalShelf { get; set; }

        [JsonProperty("enableFilterChipBarV2EagerInit")]
        public bool EnableFilterChipBarV2EagerInit { get; set; }

        [JsonProperty("enableCairoRefreshTopicIcons")]
        public bool EnableCairoRefreshTopicIcons { get; set; }

        [JsonProperty("enableServerBedtimeReminderRedesign")]
        public bool EnableServerBedtimeReminderRedesign { get; set; }

        [JsonProperty("replaceCompactTvfilmItemWithVideolockup")]
        public bool ReplaceCompactTvfilmItemWithVideolockup { get; set; }

        [JsonProperty("androidEnableSectionsForegroundChangeSets")]
        public bool AndroidEnableSectionsForegroundChangeSets { get; set; }

        [JsonProperty("androidEnableUseReliableWorkingRange")]
        public bool AndroidEnableUseReliableWorkingRange { get; set; }

        [JsonProperty("enableModernDialog")]
        public bool EnableModernDialog { get; set; }

        [JsonProperty("enableTextDialogModernization")]
        public bool EnableTextDialogModernization { get; set; }

        [JsonProperty("replaceRichGridRowVwcslotsWithVideolockup")]
        public bool ReplaceRichGridRowVwcslotsWithVideolockup { get; set; }

        [JsonProperty("removeHandleClaimingVideoVwcslots")]
        public bool RemoveHandleClaimingVideoVwcslots { get; set; }

        [JsonProperty("enableAccountLinkIconMobileWatch")]
        public bool EnableAccountLinkIconMobileWatch { get; set; }

        [JsonProperty("videoLockupUseEnvironmentOrientation")]
        public bool VideoLockupUseEnvironmentOrientation { get; set; }

        [JsonProperty("reanchorOnVisibleToRootChild")]
        public bool ReanchorOnVisibleToRootChild { get; set; }

        [JsonProperty("playlistPageHeaderUgpEnableTopBarAnimation")]
        public bool PlaylistPageHeaderUgpEnableTopBarAnimation { get; set; }

        [JsonProperty("enableCaptionsBottomSheetA11yFix")]
        public bool EnableCaptionsBottomSheetA11yFix { get; set; }

        [JsonProperty("enableModernizedBadges")]
        public bool EnableModernizedBadges { get; set; }

        [JsonProperty("enableTextDialogOnTapDismissalDedupe")]
        public bool EnableTextDialogOnTapDismissalDedupe { get; set; }

        [JsonProperty("enableElementsToggleButtonIsTogglingDisabled")]
        public bool EnableElementsToggleButtonIsTogglingDisabled { get; set; }

        [JsonProperty("disableVarispeedSliderHapticFeedback")]
        public bool DisableVarispeedSliderHapticFeedback { get; set; }

        [JsonProperty("enableModernPlayerControlsMetadataUpdate")]
        public bool EnableModernPlayerControlsMetadataUpdate { get; set; }

        [JsonProperty("readAriaLabelOnFocus")]
        public bool ReadAriaLabelOnFocus { get; set; }

        [JsonProperty("enableLocalThumbnailForVideoUploadLockups")]
        public bool EnableLocalThumbnailForVideoUploadLockups { get; set; }

        [JsonProperty("excludeDescriptionInFontSizeCalculation")]
        public bool ExcludeDescriptionInFontSizeCalculation { get; set; }

        [JsonProperty("showVideoCollaborators")]
        public bool ShowVideoCollaborators { get; set; }

        [JsonProperty("useSerialCommandForElementChips")]
        public bool UseSerialCommandForElementChips { get; set; }

        [JsonProperty("enableModernisedViewAllEndcapInShelf")]
        public bool EnableModernisedViewAllEndcapInShelf { get; set; }

        [JsonProperty("enableLocalThumbnailInThumbnailOverlay")]
        public bool EnableLocalThumbnailInThumbnailOverlay { get; set; }

        [JsonProperty("enableSnappyScrollByDefaultInHorizontalShelf")]
        public bool EnableSnappyScrollByDefaultInHorizontalShelf { get; set; }

        [JsonProperty("enableA11yFixForThreeDotMenuButton")]
        public bool EnableA11yFixForThreeDotMenuButton { get; set; }

        [JsonProperty("enableScrollableDialogHeaderAndContent")]
        public bool EnableScrollableDialogHeaderAndContent { get; set; }

        [JsonProperty("enableSingleOnTapCommandVideoLockup")]
        public bool EnableSingleOnTapCommandVideoLockup { get; set; }

        [JsonProperty("enableModernPlayerControlsSeekEdu")]
        public bool EnableModernPlayerControlsSeekEdu { get; set; }

        [JsonProperty("useOnVisibleCommandOnlyForToggleButtonAutoUpdate")]
        public bool UseOnVisibleCommandOnlyForToggleButtonAutoUpdate { get; set; }

        [JsonProperty("enableButtonTextAlignment")]
        public bool EnableButtonTextAlignment { get; set; }

        [JsonProperty("enableFilterChipBarV2ViewModel")]
        public bool EnableFilterChipBarV2ViewModel { get; set; }

        [JsonProperty("enablePageHeaderDescrptionPreviewCustomization")]
        public bool EnablePageHeaderDescrptionPreviewCustomization { get; set; }

        [JsonProperty("enableStation")]
        public bool EnableStation { get; set; }

        [JsonProperty("removeBasicContentScrollableContainer")]
        public bool RemoveBasicContentScrollableContainer { get; set; }

        [JsonProperty("enableNewShadowOnCardComponent")]
        public bool EnableNewShadowOnCardComponent { get; set; }

        [JsonProperty("enableListItemInPlaylistMetadataEditor")]
        public bool EnableListItemInPlaylistMetadataEditor { get; set; }

        [JsonProperty("enableReelsElementsActionBarDarkerShadows")]
        public bool EnableReelsElementsActionBarDarkerShadows { get; set; }

        [JsonProperty("updateSuggestedActionV2UxSpecs")]
        public bool UpdateSuggestedActionV2UxSpecs { get; set; }

        [JsonProperty("enableMoreReadablePausedStateBgColor")]
        public bool EnableMoreReadablePausedStateBgColor { get; set; }

        [JsonProperty("updateTitleSizeTo14dp")]
        public bool UpdateTitleSizeTo14dp { get; set; }

        [JsonProperty("enableRhsActionsRtlFix")]
        public bool EnableRhsActionsRtlFix { get; set; }

        [JsonProperty("enableDefaultAnimationsForReelMetapanel")]
        public bool EnableDefaultAnimationsForReelMetapanel { get; set; }

        [JsonProperty("enableShortsOverlayComponentsAttentionLogging")]
        public bool EnableShortsOverlayComponentsAttentionLogging { get; set; }

        [JsonProperty("updateReelsMetadataInnerPadding")]
        public bool UpdateReelsMetadataInnerPadding { get; set; }

        [JsonProperty("enableSuggestedActionDynamicHeight")]
        public bool EnableSuggestedActionDynamicHeight { get; set; }

        [JsonProperty("disableFlexibleActionsOnReelCarousel")]
        public bool DisableFlexibleActionsOnReelCarousel { get; set; }

        [JsonProperty("enableProductThumbnailsOverlay3pct")]
        public bool EnableProductThumbnailsOverlay3pct { get; set; }

        [JsonProperty("enableUnifiedFeaturedProductBanners")]
        public bool EnableUnifiedFeaturedProductBanners { get; set; }

        [JsonProperty("disableProductPreviewPositionTransition")]
        public bool DisableProductPreviewPositionTransition { get; set; }

        [JsonProperty("enableOutOfStockText")]
        public bool EnableOutOfStockText { get; set; }

        [JsonProperty("enableOutOfStockTextAllSurfaces")]
        public bool EnableOutOfStockTextAllSurfaces { get; set; }

        [JsonProperty("delaySecondsOnCfpRendering")]
        public int DelaySecondsOnCfpRendering { get; set; }

        [JsonProperty("bannerMaxWidthInPortraitWithVisibleControls")]
        public double BannerMaxWidthInPortraitWithVisibleControls { get; set; }

        [JsonProperty("bannerMaxWidthInLandscapeWithVisibleControls")]
        public double BannerMaxWidthInLandscapeWithVisibleControls { get; set; }

        [JsonProperty("enableMetadataRearrangementForFeaturedProductBanner")]
        public bool EnableMetadataRearrangementForFeaturedProductBanner { get; set; }

        [JsonProperty("darkThemeProductThumbnailColor")]
        public long DarkThemeProductThumbnailColor { get; set; }

        [JsonProperty("enableUnifyShortsAndVodBannerInconsistencies")]
        public bool EnableUnifyShortsAndVodBannerInconsistencies { get; set; }

        [JsonProperty("performOnceProductStickerAnimation")]
        public bool PerformOnceProductStickerAnimation { get; set; }

        [JsonProperty("enableRadioBuilderCarousel")]
        public bool EnableRadioBuilderCarousel { get; set; }

        [JsonProperty("enableLargeRecapHeaderBackgroundImage")]
        public bool EnableLargeRecapHeaderBackgroundImage { get; set; }

        [JsonProperty("enablePhotosRecapShuffleUi")]
        public bool EnablePhotosRecapShuffleUi { get; set; }

        [JsonProperty("enableA11yButtonTraitOnPlaylistsInMusicPlaylistsPickerModel")]
        public bool EnableA11yButtonTraitOnPlaylistsInMusicPlaylistsPickerModel { get; set; }

        [JsonProperty("enableElcInDownloadButtonComponent")]
        public bool EnableElcInDownloadButtonComponent { get; set; }

        [JsonProperty("enableTimedLyricsOpaqueOnStaticLayout")]
        public bool EnableTimedLyricsOpaqueOnStaticLayout { get; set; }

        [JsonProperty("enableListItemA11yScaling")]
        public bool EnableListItemA11yScaling { get; set; }

        [JsonProperty("enableAutoImageSizeInHorizontalActionCard")]
        public bool EnableAutoImageSizeInHorizontalActionCard { get; set; }

        [JsonProperty("enableMusicGridItemModernizationStyleUpdates")]
        public bool EnableMusicGridItemModernizationStyleUpdates { get; set; }

        [JsonProperty("enableIosNpiSwiftLottieOptimization")]
        public bool EnableIosNpiSwiftLottieOptimization { get; set; }

        [JsonProperty("enableMusicContainerCardFixedBgHeight")]
        public bool EnableMusicContainerCardFixedBgHeight { get; set; }

        [JsonProperty("adaptiveAnimatedLikeWatch")]
        public bool AdaptiveAnimatedLikeWatch { get; set; }

        [JsonProperty("adaptiveAnimatedLikePosts")]
        public bool AdaptiveAnimatedLikePosts { get; set; }
    }

    public class FeaturedChannelWatermarkOverlayModel
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("environment")]
        public Environment Environment { get; set; }

        [JsonProperty("playerControlsVisibilityEntityKey")]
        public string PlayerControlsVisibilityEntityKey { get; set; }

        [JsonProperty("playerTimeEntityKey")]
        public string PlayerTimeEntityKey { get; set; }

        [JsonProperty("playerLayoutStateEntityKey")]
        public string PlayerLayoutStateEntityKey { get; set; }

        [JsonProperty("playerOverlayStateEntityKey")]
        public string PlayerOverlayStateEntityKey { get; set; }
    }

    public class FeaturePlayerOverlayRenderer2
    {
        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("overlayIdentifier")]
        public string OverlayIdentifier { get; set; }

        [JsonProperty("priorityInLayer")]
        public int PriorityInLayer { get; set; }
    }

    public class Format
    {
        [JsonProperty("itag")]
        public int Itag { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("bitrate")]
        public int Bitrate { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("lastModified")]
        public string LastModified { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("xtags")]
        public string Xtags { get; set; }

        [JsonProperty("fps")]
        public int Fps { get; set; }

        [JsonProperty("qualityLabel")]
        public string QualityLabel { get; set; }

        [JsonProperty("projectionType")]
        public string ProjectionType { get; set; }

        [JsonProperty("audioQuality")]
        public string AudioQuality { get; set; }

        [JsonProperty("approxDurationMs")]
        public string ApproxDurationMs { get; set; }

        [JsonProperty("audioSampleRate")]
        public string AudioSampleRate { get; set; }

        [JsonProperty("audioChannels")]
        public int AudioChannels { get; set; }

        [JsonProperty("qualityOrdinal")]
        public string QualityOrdinal { get; set; }
    }

    public class FrameworkUpdates
    {
        [JsonProperty("entityBatchUpdate")]
        public EntityBatchUpdate EntityBatchUpdate { get; set; }

        [JsonProperty("elementUpdate")]
        public ElementUpdate ElementUpdate { get; set; }
    }

    public class Header
    {
        [JsonProperty("headerType")]
        public string HeaderType { get; set; }
    }

    public class HovercardButton
    {
        [JsonProperty("subscribeButtonRenderer")]
        public SubscribeButtonRenderer SubscribeButtonRenderer { get; set; }
    }

    public class Icon
    {
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    public class IdentifierProperties
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("uniqueLoggingIdentifier")]
        public string UniqueLoggingIdentifier { get; set; }
    }

    public class Image
    {
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    public class IndexRange
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }

    public class InitialBandwidthEstimate
    {
        [JsonProperty("detailedNetworkType")]
        public string DetailedNetworkType { get; set; }

        [JsonProperty("bandwidthBps")]
        public string BandwidthBps { get; set; }
    }

    public class InitRange
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }

    public class InnertubeCommand
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("ypcGetOfflineUpsellEndpoint")]
        public YpcGetOfflineUpsellEndpoint YpcGetOfflineUpsellEndpoint { get; set; }
    }

    public class InteractionLoggingCommandMetadata
    {
        [JsonProperty("loggingExpectations")]
        public LoggingExpectations LoggingExpectations { get; set; }
    }

    public class KeepSubscriptionButtonText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class Label
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class LidarSdkConfig
    {
        [JsonProperty("enableActiveViewReporter")]
        public bool EnableActiveViewReporter { get; set; }

        [JsonProperty("useMediaTime")]
        public bool UseMediaTime { get; set; }

        [JsonProperty("sendTosMetrics")]
        public bool SendTosMetrics { get; set; }

        [JsonProperty("usePlayerState")]
        public bool UsePlayerState { get; set; }

        [JsonProperty("enableIosAppStateCheck")]
        public bool EnableIosAppStateCheck { get; set; }

        [JsonProperty("enableImprovedSizeReportingAndroid")]
        public bool EnableImprovedSizeReportingAndroid { get; set; }

        [JsonProperty("enableIsAndroidVideoAlwaysMeasurable")]
        public bool EnableIsAndroidVideoAlwaysMeasurable { get; set; }

        [JsonProperty("enableActiveViewAudioMeasurementAndroid")]
        public bool EnableActiveViewAudioMeasurementAndroid { get; set; }
    }

    public class LoggingContext
    {
        [JsonProperty("vssLoggingContext")]
        public VssLoggingContext VssLoggingContext { get; set; }
    }

    public class LoggingDirectives
    {
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }
    }

    public class LoggingExpectations
    {
        [JsonProperty("screenCreatedLoggingExpectations")]
        public ScreenCreatedLoggingExpectations ScreenCreatedLoggingExpectations { get; set; }
    }

    public class MainAppAdaptiveContext
    {
        [JsonProperty("animationDecisions")]
        public List<AnimationDecision> AnimationDecisions { get; set; }

        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }
    }

    public class MainAppContext
    {
        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }

        [JsonProperty("clientName")]
        public string ClientName { get; set; }
    }

    public class Mapping
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("resultField")]
        public int ResultField { get; set; }

        [JsonProperty("resultPath")]
        public List<int> ResultPath { get; set; }
    }

    public class MediaCacheConfig
    {
        [JsonProperty("cacheLoadPolicy")]
        public CacheLoadPolicy CacheLoadPolicy { get; set; }
    }

    public class MediaCommonConfig
    {
        [JsonProperty("dynamicReadaheadConfig")]
        public DynamicReadaheadConfig DynamicReadaheadConfig { get; set; }

        [JsonProperty("mediaUstreamerRequestConfig")]
        public MediaUstreamerRequestConfig MediaUstreamerRequestConfig { get; set; }

        [JsonProperty("predictedReadaheadConfig")]
        public PredictedReadaheadConfig PredictedReadaheadConfig { get; set; }

        [JsonProperty("mediaFetchRetryConfig")]
        public MediaFetchRetryConfig MediaFetchRetryConfig { get; set; }

        [JsonProperty("mediaFetchMaximumServerErrors")]
        public int MediaFetchMaximumServerErrors { get; set; }

        [JsonProperty("mediaFetchMaximumNetworkErrors")]
        public int MediaFetchMaximumNetworkErrors { get; set; }

        [JsonProperty("mediaFetchMaximumErrors")]
        public int MediaFetchMaximumErrors { get; set; }

        [JsonProperty("serverReadaheadConfig")]
        public ServerReadaheadConfig ServerReadaheadConfig { get; set; }

        [JsonProperty("useServerDrivenAbr")]
        public bool UseServerDrivenAbr { get; set; }

        [JsonProperty("sabrClientConfig")]
        public SabrClientConfig SabrClientConfig { get; set; }

        [JsonProperty("serverPlaybackStartConfig")]
        public ServerPlaybackStartConfig ServerPlaybackStartConfig { get; set; }

        [JsonProperty("usePlatypus")]
        public bool UsePlatypus { get; set; }

        [JsonProperty("mediaCacheConfig")]
        public MediaCacheConfig MediaCacheConfig { get; set; }

        [JsonProperty("bandwidthEstimationConfig")]
        public BandwidthEstimationConfig BandwidthEstimationConfig { get; set; }

        [JsonProperty("platypusUseEnvoyNetFetch")]
        public bool PlatypusUseEnvoyNetFetch { get; set; }

        [JsonProperty("fixLivePlaybackModelDefaultPosition")]
        public bool FixLivePlaybackModelDefaultPosition { get; set; }
    }

    public class MediaFetchRetryConfig
    {
        [JsonProperty("initialDelayMs")]
        public int InitialDelayMs { get; set; }

        [JsonProperty("backoffFactor")]
        public double BackoffFactor { get; set; }

        [JsonProperty("maximumDelayMs")]
        public int MaximumDelayMs { get; set; }

        [JsonProperty("jitterFactor")]
        public double JitterFactor { get; set; }
    }

    public class MediaUstreamerRequestConfig
    {
        [JsonProperty("enableVideoPlaybackRequest")]
        public bool EnableVideoPlaybackRequest { get; set; }

        [JsonProperty("videoPlaybackUstreamerConfig")]
        public string VideoPlaybackUstreamerConfig { get; set; }

        [JsonProperty("videoPlaybackPostEmptyBody")]
        public bool VideoPlaybackPostEmptyBody { get; set; }

        [JsonProperty("isVideoPlaybackRequestIdempotent")]
        public bool IsVideoPlaybackRequestIdempotent { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }

        [JsonProperty("servingCreationDate")]
        public ServingCreationDate ServingCreationDate { get; set; }
    }

    public class MetadataFormat
    {
    }

    public class Miniplayer
    {
        [JsonProperty("miniplayerRenderer")]
        public MiniplayerRenderer MiniplayerRenderer { get; set; }
    }

    public class MiniplayerRenderer
    {
        [JsonProperty("playbackMode")]
        public string PlaybackMode { get; set; }
    }

    public class MobileEomFlowState
    {
        [JsonProperty("updatedVisitorData")]
        public string UpdatedVisitorData { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }
    }

    public class Model
    {
        [JsonProperty("featuredChannelWatermarkOverlayModel")]
        public FeaturedChannelWatermarkOverlayModel FeaturedChannelWatermarkOverlayModel { get; set; }
    }

    public class MusicContext
    {
        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }
    }

    public class Mutation
    {
        [JsonProperty("entityKey")]
        public string EntityKey { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }

    public class NearestRankConfig
    {
        [JsonProperty("slidingWindowSize")]
        public int SlidingWindowSize { get; set; }

        [JsonProperty("percentile")]
        public double Percentile { get; set; }

        [JsonProperty("scalar")]
        public double Scalar { get; set; }
    }

    public class NetworkProtocolConfig
    {
        [JsonProperty("useQuic")]
        public bool UseQuic { get; set; }
    }

    public class NewElement
    {
        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class NextRequestPolicy
    {
        [JsonProperty("targetAudioReadaheadMs")]
        public int TargetAudioReadaheadMs { get; set; }

        [JsonProperty("targetVideoReadaheadMs")]
        public int TargetVideoReadaheadMs { get; set; }
    }

    public class Offlineability
    {
        [JsonProperty("buttonRenderer")]
        public ButtonRenderer ButtonRenderer { get; set; }
    }

    public class OfflineabilityEntity
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("addToOfflineButtonState")]
        public string AddToOfflineButtonState { get; set; }

        [JsonProperty("commandWrapper")]
        public CommandWrapper CommandWrapper { get; set; }

        [JsonProperty("contentCheckOk")]
        public bool ContentCheckOk { get; set; }

        [JsonProperty("racyCheckOk")]
        public bool RacyCheckOk { get; set; }

        [JsonProperty("loggingDirectives")]
        public LoggingDirectives LoggingDirectives { get; set; }
    }

    public class OnFailureCommand
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("updateEomStateCommand")]
        public UpdateEomStateCommand UpdateEomStateCommand { get; set; }
    }

    public class OnResponseReceivedAction
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("startEomFlowCommand")]
        public StartEomFlowCommand StartEomFlowCommand { get; set; }
    }

    public class PaidChannelUnsubscribeMessageRenderer
    {
        [JsonProperty("unsubscribeMessage")]
        public UnsubscribeMessage UnsubscribeMessage { get; set; }

        [JsonProperty("keepSubscriptionButtonText")]
        public KeepSubscriptionButtonText KeepSubscriptionButtonText { get; set; }

        [JsonProperty("unsubscriptionAllowed")]
        public bool UnsubscriptionAllowed { get; set; }

        [JsonProperty("unsubscribeButtonText")]
        public UnsubscribeButtonText UnsubscribeButtonText { get; set; }
    }

    public class Param
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Payload
    {
        [JsonProperty("offlineabilityEntity")]
        public OfflineabilityEntity OfflineabilityEntity { get; set; }
    }

    public class PlayabilityStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("playableInEmbed")]
        public bool PlayableInEmbed { get; set; }

        [JsonProperty("offlineability")]
        public Offlineability Offlineability { get; set; }

        [JsonProperty("miniplayer")]
        public Miniplayer Miniplayer { get; set; }

        [JsonProperty("contextParams")]
        public string ContextParams { get; set; }
    }

    public class PlaybackOnesieConfig
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("exoPlayerInitConfig")]
        public ExoPlayerInitConfig ExoPlayerInitConfig { get; set; }

        [JsonProperty("playerInitConfig")]
        public PlayerInitConfig PlayerInitConfig { get; set; }

        [JsonProperty("dataSaverConfig")]
        public DataSaverConfig DataSaverConfig { get; set; }

        [JsonProperty("commonConfig")]
        public CommonConfig CommonConfig { get; set; }
    }

    public class PlaybackStartConfig
    {
        [JsonProperty("startTimeToleranceBeforeMs")]
        public string StartTimeToleranceBeforeMs { get; set; }
    }

    public class PlaybackStartPolicy
    {
        [JsonProperty("startMinReadaheadPolicy")]
        public List<StartMinReadaheadPolicy> StartMinReadaheadPolicy { get; set; }
    }

    public class PlaybackTracking
    {
        [JsonProperty("videostatsPlaybackUrl")]
        public VideostatsPlaybackUrl VideostatsPlaybackUrl { get; set; }

        [JsonProperty("videostatsDelayplayUrl")]
        public VideostatsDelayplayUrl VideostatsDelayplayUrl { get; set; }

        [JsonProperty("videostatsWatchtimeUrl")]
        public VideostatsWatchtimeUrl VideostatsWatchtimeUrl { get; set; }

        [JsonProperty("ptrackingUrl")]
        public PtrackingUrl PtrackingUrl { get; set; }

        [JsonProperty("qoeUrl")]
        public QoeUrl QoeUrl { get; set; }

        [JsonProperty("atrUrl")]
        public AtrUrl AtrUrl { get; set; }

        [JsonProperty("engageUrl")]
        public EngageUrl EngageUrl { get; set; }

        [JsonProperty("videostatsScheduledFlushWalltimeSeconds")]
        public List<int> VideostatsScheduledFlushWalltimeSeconds { get; set; }

        [JsonProperty("videostatsDefaultFlushIntervalSeconds")]
        public int VideostatsDefaultFlushIntervalSeconds { get; set; }
    }

    public class PlayerAttestationRenderer
    {
        [JsonProperty("challenge")]
        public string Challenge { get; set; }
    }

    public class PlayerConfig
    {
        [JsonProperty("audioConfig")]
        public AudioConfig AudioConfig { get; set; }

        [JsonProperty("exoPlayerConfig")]
        public ExoPlayerConfig ExoPlayerConfig { get; set; }

        [JsonProperty("playbackStartConfig")]
        public PlaybackStartConfig PlaybackStartConfig { get; set; }

        [JsonProperty("adRequestConfig")]
        public AdRequestConfig AdRequestConfig { get; set; }

        [JsonProperty("networkProtocolConfig")]
        public NetworkProtocolConfig NetworkProtocolConfig { get; set; }

        [JsonProperty("androidNetworkStackConfig")]
        public AndroidNetworkStackConfig AndroidNetworkStackConfig { get; set; }

        [JsonProperty("lidarSdkConfig")]
        public LidarSdkConfig LidarSdkConfig { get; set; }

        [JsonProperty("androidMedialibConfig")]
        public AndroidMedialibConfig AndroidMedialibConfig { get; set; }

        [JsonProperty("playerControlsConfig")]
        public PlayerControlsConfig PlayerControlsConfig { get; set; }

        [JsonProperty("variableSpeedConfig")]
        public VariableSpeedConfig VariableSpeedConfig { get; set; }

        [JsonProperty("decodeQualityConfig")]
        public DecodeQualityConfig DecodeQualityConfig { get; set; }

        [JsonProperty("vrConfig")]
        public VrConfig VrConfig { get; set; }

        [JsonProperty("qoeStatsClientConfig")]
        public QoeStatsClientConfig QoeStatsClientConfig { get; set; }

        [JsonProperty("androidPlayerStatsConfig")]
        public AndroidPlayerStatsConfig AndroidPlayerStatsConfig { get; set; }

        [JsonProperty("stickyQualitySelectionConfig")]
        public StickyQualitySelectionConfig StickyQualitySelectionConfig { get; set; }

        [JsonProperty("adSurveyRequestConfig")]
        public AdSurveyRequestConfig AdSurveyRequestConfig { get; set; }

        [JsonProperty("retryConfig")]
        public RetryConfig RetryConfig { get; set; }

        [JsonProperty("cmsPathProbeConfig")]
        public CmsPathProbeConfig CmsPathProbeConfig { get; set; }

        [JsonProperty("mediaCommonConfig")]
        public MediaCommonConfig MediaCommonConfig { get; set; }

        [JsonProperty("playerGestureConfig")]
        public PlayerGestureConfig PlayerGestureConfig { get; set; }

        [JsonProperty("taskCoordinatorConfig")]
        public TaskCoordinatorConfig TaskCoordinatorConfig { get; set; }
    }

    public class PlayerControlsConfig
    {
        [JsonProperty("showCachedInTimebar")]
        public bool ShowCachedInTimebar { get; set; }
    }

    public class PlayerGestureConfig
    {
        [JsonProperty("downAndOutLandscapeAllowed")]
        public bool DownAndOutLandscapeAllowed { get; set; }

        [JsonProperty("downAndOutPortraitAllowed")]
        public bool DownAndOutPortraitAllowed { get; set; }
    }

    public class PlayerInitConfig
    {
        [JsonProperty("stickyQualitySelectionConfig")]
        public StickyQualitySelectionConfig StickyQualitySelectionConfig { get; set; }
    }

    public class PlayerSettingsMenuData
    {
        [JsonProperty("loggingDirectives")]
        public LoggingDirectives LoggingDirectives { get; set; }
    }

    public class PlayerStoryboardSpecRenderer
    {
        [JsonProperty("spec")]
        public string Spec { get; set; }

        [JsonProperty("recommendedLevel")]
        public int RecommendedLevel { get; set; }
    }

    public class PlaylistLength
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class PredictedReadaheadConfig
    {
        [JsonProperty("minReadaheadMs")]
        public int MinReadaheadMs { get; set; }

        [JsonProperty("maxReadaheadMs")]
        public int MaxReadaheadMs { get; set; }
    }

    public class Properties
    {
        [JsonProperty("identifierProperties")]
        public IdentifierProperties IdentifierProperties { get; set; }
    }

    public class PtrackingUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class QoeStatsClientConfig
    {
        [JsonProperty("batchedEntriesPeriodMs")]
        public string BatchedEntriesPeriodMs { get; set; }
    }

    public class QoeUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class ReelsPlayerContext
    {
        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }
    }

    public class Resource
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }

    public class ResourceStatus
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ResourceStatusInResponseCheck
    {
        [JsonProperty("resourceStatuses")]
        public List<ResourceStatus> ResourceStatuses { get; set; }

        [JsonProperty("serverBuildLabel")]
        public string ServerBuildLabel { get; set; }
    }

    public class ResponseContext
    {
        [JsonProperty("visitorData")]
        public string VisitorData { get; set; }

        [JsonProperty("serviceTrackingParams")]
        public List<ServiceTrackingParam> ServiceTrackingParams { get; set; }

        [JsonProperty("maxAgeSeconds")]
        public int MaxAgeSeconds { get; set; }

        [JsonProperty("rolloutToken")]
        public string RolloutToken { get; set; }
    }

    public class RetryConfig
    {
        [JsonProperty("retryEligibleErrors")]
        public List<string> RetryEligibleErrors { get; set; }

        [JsonProperty("retryUnderSameConditionAttempts")]
        public int RetryUnderSameConditionAttempts { get; set; }

        [JsonProperty("retryWithNewSurfaceAttempts")]
        public int RetryWithNewSurfaceAttempts { get; set; }

        [JsonProperty("progressiveFallbackOnNonNetworkErrors")]
        public bool ProgressiveFallbackOnNonNetworkErrors { get; set; }

        [JsonProperty("l3FallbackOnDrmErrors")]
        public bool L3FallbackOnDrmErrors { get; set; }

        [JsonProperty("retryAfterCacheRemoval")]
        public bool RetryAfterCacheRemoval { get; set; }

        [JsonProperty("widevineL3EnforcedFallbackOnDrmErrors")]
        public bool WidevineL3EnforcedFallbackOnDrmErrors { get; set; }

        [JsonProperty("exoProxyableFormatFallback")]
        public bool ExoProxyableFormatFallback { get; set; }

        [JsonProperty("maxPlayerRetriesWhenNetworkUnavailable")]
        public int MaxPlayerRetriesWhenNetworkUnavailable { get; set; }

        [JsonProperty("retryWithLibvpx")]
        public bool RetryWithLibvpx { get; set; }

        [JsonProperty("suppressFatalErrorAfterStop")]
        public bool SuppressFatalErrorAfterStop { get; set; }

        [JsonProperty("fallbackFromHfrToSfrOnFormatDecodeError")]
        public bool FallbackFromHfrToSfrOnFormatDecodeError { get; set; }

        [JsonProperty("fallbackToSwDecoderOnFormatDecodeError")]
        public bool FallbackToSwDecoderOnFormatDecodeError { get; set; }
    }

    public class Root
    {
        // [JsonProperty("responseContext")]
        // public ResponseContext ResponseContext { get; set; }

        [JsonProperty("playabilityStatus")]
        public PlayabilityStatus PlayabilityStatus { get; set; }

        [JsonProperty("streamingData")]
        public StreamingData StreamingData { get; set; }

        [JsonProperty("playbackTracking")]
        public PlaybackTracking PlaybackTracking { get; set; }

        [JsonProperty("videoDetails")]
        public VideoDetails VideoDetails { get; set; }

        [JsonProperty("playerConfig")]
        public PlayerConfig PlayerConfig { get; set; }

        [JsonProperty("storyboards")]
        public Storyboards Storyboards { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("attestation")]
        public Attestation Attestation { get; set; }

        [JsonProperty("endscreen")]
        public Endscreen Endscreen { get; set; }

        [JsonProperty("onResponseReceivedActions")]
        public List<OnResponseReceivedAction> OnResponseReceivedActions { get; set; }

        [JsonProperty("playerSettingsMenuData")]
        public PlayerSettingsMenuData PlayerSettingsMenuData { get; set; }

        [JsonProperty("adBreakHeartbeatParams")]
        public string AdBreakHeartbeatParams { get; set; }

        [JsonProperty("frameworkUpdates")]
        public FrameworkUpdates FrameworkUpdates { get; set; }
    }

    public class Run
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class SabrClientConfig
    {
        [JsonProperty("defaultBackOffTimeMs")]
        public int DefaultBackOffTimeMs { get; set; }

        [JsonProperty("enableHostFallback")]
        public bool EnableHostFallback { get; set; }

        [JsonProperty("primaryProbingDelayMs")]
        public int PrimaryProbingDelayMs { get; set; }

        [JsonProperty("maxFailureAttemptsBeforeFallback")]
        public int MaxFailureAttemptsBeforeFallback { get; set; }

        [JsonProperty("enableServerInitiatedHostFallback")]
        public bool EnableServerInitiatedHostFallback { get; set; }
    }

    public class ScreenCreatedLoggingExpectations
    {
        [JsonProperty("expectedParentScreens")]
        public List<ExpectedParentScreen> ExpectedParentScreens { get; set; }
    }

    public class ServerPlaybackStartConfig
    {
        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("playbackStartPolicy")]
        public PlaybackStartPolicy PlaybackStartPolicy { get; set; }
    }

    public class ServerReadaheadConfig
    {
        [JsonProperty("nextRequestPolicy")]
        public NextRequestPolicy NextRequestPolicy { get; set; }
    }

    public class ServiceEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("ypcGetOfflineUpsellEndpoint")]
        public YpcGetOfflineUpsellEndpoint YpcGetOfflineUpsellEndpoint { get; set; }
    }

    public class ServiceEndpoint2
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("subscribeEndpoint")]
        public SubscribeEndpoint SubscribeEndpoint { get; set; }

        [JsonProperty("unsubscribeEndpoint")]
        public UnsubscribeEndpoint UnsubscribeEndpoint { get; set; }
    }

    public class ServiceTrackingParam
    {
        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("params")]
        public List<Param> Params { get; set; }
    }

    public class ServingCreationDate
    {
        [JsonProperty("seconds")]
        public string Seconds { get; set; }

        [JsonProperty("nanos")]
        public int Nanos { get; set; }
    }

    public class ShoppingAppContext
    {
        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }
    }

    public class SignInEndpoint
    {
        [JsonProperty("hack")]
        public bool Hack { get; set; }
    }

    public class Source
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class StartEomFlowCommand
    {
        [JsonProperty("eomFlowRenderer")]
        public EomFlowRenderer EomFlowRenderer { get; set; }

        [JsonProperty("consentMoment")]
        public string ConsentMoment { get; set; }
    }

    public class StartMinReadaheadPolicy
    {
        [JsonProperty("minReadaheadMs")]
        public int MinReadaheadMs { get; set; }
    }

    public class StaticDataResources
    {
        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }

    public class StaticDeviceEnvDataContext
    {
        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        [JsonProperty("platformName")]
        public string PlatformName { get; set; }
    }

    public class StickyQualitySelectionConfig
    {
        [JsonProperty("stickySelectionType")]
        public string StickySelectionType { get; set; }

        [JsonProperty("expirationTimeSinceLastManualVideoQualitySelectionMs")]
        public string ExpirationTimeSinceLastManualVideoQualitySelectionMs { get; set; }

        [JsonProperty("expirationTimeSinceLastPlaybackStartMs")]
        public string ExpirationTimeSinceLastPlaybackStartMs { get; set; }

        [JsonProperty("stickyCeilingOverridesSimpleBitrateCap")]
        public bool StickyCeilingOverridesSimpleBitrateCap { get; set; }
    }

    public class Storyboards
    {
        [JsonProperty("playerStoryboardSpecRenderer")]
        public PlayerStoryboardSpecRenderer PlayerStoryboardSpecRenderer { get; set; }
    }

    public class StreamingData
    {
        [JsonProperty("expiresInSeconds")]
        public string ExpiresInSeconds { get; set; }

        [JsonProperty("formats")]
        public List<Format> Formats { get; set; }

        [JsonProperty("adaptiveFormats")]
        public List<AdaptiveFormat> AdaptiveFormats { get; set; }

        [JsonProperty("serverAbrStreamingUrl")]
        public string ServerAbrStreamingUrl { get; set; }

        [JsonProperty("metadataFormats")]
        public List<MetadataFormat> MetadataFormats { get; set; }
    }

    public class Style
    {
        [JsonProperty("styleType")]
        public string StyleType { get; set; }

        [JsonProperty("suppressFreeIcon")]
        public bool SuppressFreeIcon { get; set; }
    }

    public class SubscribeAccessibility
    {
        [JsonProperty("accessibilityData")]
        public AccessibilityData AccessibilityData { get; set; }
    }

    public class SubscribeButtonRenderer
    {
        [JsonProperty("buttonText")]
        public ButtonText ButtonText { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("showPreferences")]
        public bool ShowPreferences { get; set; }

        [JsonProperty("unsubscribeMessage")]
        public UnsubscribeMessage UnsubscribeMessage { get; set; }

        [JsonProperty("subscribedButtonText")]
        public SubscribedButtonText SubscribedButtonText { get; set; }

        [JsonProperty("unsubscribedButtonText")]
        public UnsubscribedButtonText UnsubscribedButtonText { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("unsubscribeButtonText")]
        public UnsubscribeButtonText UnsubscribeButtonText { get; set; }

        [JsonProperty("serviceEndpoints")]
        public List<ServiceEndpoint> ServiceEndpoints { get; set; }

        [JsonProperty("style")]
        public Style Style { get; set; }

        [JsonProperty("subscribeAccessibility")]
        public SubscribeAccessibility SubscribeAccessibility { get; set; }

        [JsonProperty("unsubscribeAccessibility")]
        public UnsubscribeAccessibility UnsubscribeAccessibility { get; set; }

        [JsonProperty("serverTimestampMs")]
        public string ServerTimestampMs { get; set; }
    }

    public class SubscribedButtonText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class SubscribeEndpoint
    {
        [JsonProperty("channelIds")]
        public List<string> ChannelIds { get; set; }

        [JsonProperty("params")]
        public string Params { get; set; }
    }

    public class Subscription
    {
        [JsonProperty("environmentDataFilter")]
        public List<string> EnvironmentDataFilter { get; set; }
    }

    public class SubscriptionConfig
    {
        [JsonProperty("dataStoreSubscriptionConfig")]
        public DataStoreSubscriptionConfig DataStoreSubscriptionConfig { get; set; }

        [JsonProperty("environmentSubscriptionConfig")]
        public EnvironmentSubscriptionConfig EnvironmentSubscriptionConfig { get; set; }

        [JsonProperty("themeSubscriptionConfig")]
        public ThemeSubscriptionConfig ThemeSubscriptionConfig { get; set; }
    }

    public class SubscriptionsContext
    {
        [JsonProperty("experiments")]
        public Experiments Experiments { get; set; }
    }

    public class TaskCoordinatorConfig
    {
        [JsonProperty("prefetchCoordinatorBufferedPositionMillisRelease")]
        public int PrefetchCoordinatorBufferedPositionMillisRelease { get; set; }

        [JsonProperty("prefetchCoordinatorBufferedPositionMillisPause")]
        public int PrefetchCoordinatorBufferedPositionMillisPause { get; set; }
    }

    public class TemplateConfig
    {
        [JsonProperty("uriTemplateConfig")]
        public UriTemplateConfig UriTemplateConfig { get; set; }
    }

    public class TemplateUpdate
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("serializedTemplateConfig")]
        public string SerializedTemplateConfig { get; set; }

        [JsonProperty("resourceTag")]
        public string ResourceTag { get; set; }

        [JsonProperty("templateType")]
        public string TemplateType { get; set; }
    }

    public class Text
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }

        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
    }

    public class ThemeSubscriptionConfig
    {
        [JsonProperty("mappings")]
        public List<Mapping> Mappings { get; set; }

        [JsonProperty("resultField")]
        public int ResultField { get; set; }
    }

    public class ThemeUpdate
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("resourceTag")]
        public string ResourceTag { get; set; }

        [JsonProperty("themeBytes")]
        public string ThemeBytes { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("thumbnails")]
        public List<Thumbnail2> Thumbnails { get; set; }
    }

    public class Thumbnail2
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class ThumbnailOverlay
    {
        [JsonProperty("thumbnailOverlayTimeStatusRenderer")]
        public ThumbnailOverlayTimeStatusRenderer ThumbnailOverlayTimeStatusRenderer { get; set; }
    }

    public class ThumbnailOverlayTimeStatusRenderer
    {
        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }
    }

    public class Timestamp
    {
        [JsonProperty("seconds")]
        public string Seconds { get; set; }

        [JsonProperty("nanos")]
        public int Nanos { get; set; }
    }

    public class Title
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }

        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
    }

    public class Type
    {
        [JsonProperty("componentType")]
        public ComponentType ComponentType { get; set; }
    }

    public class TypographyContext
    {
    }

    public class UnsubscribeAccessibility
    {
        [JsonProperty("accessibilityData")]
        public AccessibilityData AccessibilityData { get; set; }
    }

    public class UnsubscribeButtonText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class UnsubscribedButtonText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class UnsubscribeEndpoint
    {
        [JsonProperty("channelIds")]
        public List<string> ChannelIds { get; set; }

        [JsonProperty("params")]
        public string Params { get; set; }
    }

    public class UnsubscribeMessage
    {
        [JsonProperty("paidChannelUnsubscribeMessageRenderer")]
        public PaidChannelUnsubscribeMessageRenderer PaidChannelUnsubscribeMessageRenderer { get; set; }

        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class Update
    {
        [JsonProperty("templateUpdate")]
        public TemplateUpdate TemplateUpdate { get; set; }

        [JsonProperty("themeUpdate")]
        public ThemeUpdate ThemeUpdate { get; set; }

        [JsonProperty("capabilitiesUpdate")]
        public CapabilitiesUpdate CapabilitiesUpdate { get; set; }

        [JsonProperty("staticDataResources")]
        public StaticDataResources StaticDataResources { get; set; }

        [JsonProperty("resourceStatusInResponseCheck")]
        public ResourceStatusInResponseCheck ResourceStatusInResponseCheck { get; set; }
    }

    public class UpdateEomStateCommand
    {
        [JsonProperty("mobileEomFlowState")]
        public MobileEomFlowState MobileEomFlowState { get; set; }

        [JsonProperty("hack")]
        public bool Hack { get; set; }
    }

    public class UriTemplateConfig
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Url
    {
        [JsonProperty("privateDoNotAccessOrElseTrustedResourceUrlWrappedValue")]
        public string PrivateDoNotAccessOrElseTrustedResourceUrlWrappedValue { get; set; }
    }

    public class Value
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("updateEomStateCommand")]
        public UpdateEomStateCommand UpdateEomStateCommand { get; set; }

        [JsonProperty("signInEndpoint")]
        public SignInEndpoint SignInEndpoint { get; set; }
    }

    public class VariableSpeedConfig
    {
        [JsonProperty("availablePlaybackSpeeds")]
        public List<AvailablePlaybackSpeed> AvailablePlaybackSpeeds { get; set; }

        [JsonProperty("androidVariableSpeedTimeoutSecs")]
        public int AndroidVariableSpeedTimeoutSecs { get; set; }

        [JsonProperty("enableVariableSpeedOnOtf")]
        public bool EnableVariableSpeedOnOtf { get; set; }
    }

    public class VideoDetails
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lengthSeconds")]
        public string LengthSeconds { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("isOwnerViewing")]
        public bool IsOwnerViewing { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("isCrawlable")]
        public bool IsCrawlable { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("allowRatings")]
        public bool AllowRatings { get; set; }

        [JsonProperty("viewCount")]
        public string ViewCount { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty("isUnpluggedCorpus")]
        public bool IsUnpluggedCorpus { get; set; }

        [JsonProperty("isLiveContent")]
        public bool IsLiveContent { get; set; }
    }

    public class VideostatsDelayplayUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class VideostatsPlaybackUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class VideostatsWatchtimeUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }
    }

    public class Visibility
    {
        [JsonProperty("types")]
        public string Types { get; set; }
    }

    public class VrConfig
    {
        [JsonProperty("allowVr")]
        public bool AllowVr { get; set; }

        [JsonProperty("allowSubtitles")]
        public bool AllowSubtitles { get; set; }

        [JsonProperty("showHqButton")]
        public bool ShowHqButton { get; set; }

        [JsonProperty("sphericalDirectionLoggingEnabled")]
        public bool SphericalDirectionLoggingEnabled { get; set; }

        [JsonProperty("enableAndroidVr180MagicWindow")]
        public bool EnableAndroidVr180MagicWindow { get; set; }

        [JsonProperty("enableAndroidMagicWindowEduOverlay")]
        public bool EnableAndroidMagicWindowEduOverlay { get; set; }

        [JsonProperty("magicWindowEduOverlayText")]
        public string MagicWindowEduOverlayText { get; set; }

        [JsonProperty("magicWindowEduOverlayAnimationUrl")]
        public string MagicWindowEduOverlayAnimationUrl { get; set; }
    }

    public class VssLoggingContext
    {
        [JsonProperty("serializedContextData")]
        public string SerializedContextData { get; set; }
    }

    public class WatchEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playerParams")]
        public string PlayerParams { get; set; }

        [JsonProperty("watchEndpointSupportedOnesieConfig")]
        public WatchEndpointSupportedOnesieConfig WatchEndpointSupportedOnesieConfig { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("loggingContext")]
        public LoggingContext LoggingContext { get; set; }
    }

    public class WatchEndpointSupportedOnesieConfig
    {
        [JsonProperty("playbackOnesieConfig")]
        public PlaybackOnesieConfig PlaybackOnesieConfig { get; set; }
    }

    public class Watermark
    {
        [JsonProperty("sources")]
        public List<Source> Sources { get; set; }

        [JsonProperty("contentMode")]
        public string ContentMode { get; set; }
    }

    public class WebToNativeMessageMap
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class WebViewRenderer
    {
        [JsonProperty("url")]
        public Url Url { get; set; }

        [JsonProperty("onFailureCommand")]
        public OnFailureCommand OnFailureCommand { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("webViewEntityKey")]
        public string WebViewEntityKey { get; set; }

        [JsonProperty("webToNativeMessageMap")]
        public List<WebToNativeMessageMap> WebToNativeMessageMap { get; set; }

        [JsonProperty("webViewUseCase")]
        public string WebViewUseCase { get; set; }

        [JsonProperty("openInBrowserUrls")]
        public List<string> OpenInBrowserUrls { get; set; }

        [JsonProperty("firstPartyHostNameAllowList")]
        public List<string> FirstPartyHostNameAllowList { get; set; }
    }

    public class YpcGetOfflineUpsellEndpoint
    {
        [JsonProperty("params")]
        public string Params { get; set; }
    }
}