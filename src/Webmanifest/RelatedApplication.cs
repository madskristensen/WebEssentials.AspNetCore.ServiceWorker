using Newtonsoft.Json;

namespace WebEssentials.AspNetCore.Pwa
{
    /// <summary>
    /// A related native application.
    /// </summary>
    public class RelatedApplication
    {
        /// <summary>The platform on which the application can be found.</summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

        /// <summary>The URL at which the application can be found.</summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>The ID used to represent the application on the specified platform.</summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
