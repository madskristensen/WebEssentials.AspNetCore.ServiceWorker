using Microsoft.Extensions.Configuration;
using System;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// Options for the service worker.
    /// </summary>
    public class ServiceWorkerOptions
    {
        /// <summary>
        /// Creates a new default instance of the options.
        /// </summary>
        public ServiceWorkerOptions()
        {
            Version = "v1.0";
            Mode = ServiceWorkerMode.Safe;
            RoutesToPreCache = string.Empty;
        }

        internal ServiceWorkerOptions(IConfiguration config)
            : this()
        {
            Version = config["serviceworker:version"] ?? Version;
            RoutesToPreCache = config["serviceworker:routesToPreCache"] ?? RoutesToPreCache;

            if (Enum.TryParse(config["serviceworker:mode"] ?? "safe", true, out ServiceWorkerMode mode))
            {
                Mode = mode;
            }
        }

        /// <summary>
        /// The version of the service worker as well as the name of the cache.
        /// Change this property to force the service worker to reload in browsers.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Selects one of the predefined service worker types.
        /// </summary>
        public ServiceWorkerMode Mode { get; set; }

        /// <summary>
        /// A comma separated list of routes to pre-cache when service worker installs in the browser.
        /// </summary>
        public string RoutesToPreCache { get; set; }
    }
}
