using Newtonsoft.Json;

namespace WebEssentials.AspNetCore.Pwa
{
    /// <summary>
    /// An icon as defined in the web manifest
    /// </summary>
    public class Icon
    {
        /// <summary>The path to the image file. If src is a relative URL, the base URL will be the URL of the manifest. Example: "/img/icon-192x192.png"</summary>
        [JsonProperty("src")]
        public string Src { get; set; }

        /// <summary>A hint as to the media type of the image.The purpose of this member is to allow a user agent to quickly ignore images of media types it does not support. Example: "image/png"</summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>A string containing space-separated image dimensions. Example: "192x192"</summary>
        [JsonProperty("sizes")]
        public string Sizes { get; set; }
    }
}