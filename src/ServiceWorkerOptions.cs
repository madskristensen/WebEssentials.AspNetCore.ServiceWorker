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
            CacheId = "v1.0";
            Strategy = ServiceWorkerStrategy.NetworkFirstFallbackCacheSafe;
            RoutesToPreCache = string.Empty;
            OfflineRoute = "/offline.html";
            RegisterServiceWorker = true;
        }

        internal ServiceWorkerOptions(IConfiguration config)
            : this()
        {
            CacheId = config["serviceworker:version"] ?? CacheId;
            RoutesToPreCache = config["serviceworker:routesToPreCache"] ?? RoutesToPreCache;
            OfflineRoute= config["serviceworker:offlineRoute"] ?? OfflineRoute;

            if (bool.TryParse(config["serviceworker:registerServiceWorker"] ?? "true", out bool register))
            {
                RegisterServiceWorker = register;
            }

            if (Enum.TryParse(config["serviceworker:mode"] ?? "safe", true, out ServiceWorkerStrategy mode))
            {
                Strategy = mode;
            }
        }

        /// <summary>
        /// The cache identifier of the service worker (can be any string).
        /// Change this property to force the service worker to reload in browsers.
        /// </summary>
        public string CacheId { get; set; }

        /// <summary>
        /// Selects one of the predefined service worker types.
        /// </summary>
        public ServiceWorkerStrategy Strategy { get; set; }

        /// <summary>
        /// A comma separated list of routes to pre-cache when service worker installs in the browser.
        /// </summary>
        public string RoutesToPreCache { get; set; }

        /// <summary>
        /// The route to the page to show when offline
        /// </summary>
        public string OfflineRoute { get; set; }

        /// <summary>
        /// Determines if a script that registers the service worker should be injected
        /// into the bottom of the HTML page.
        /// </summary>
        public bool RegisterServiceWorker { get; set; }
    }
}
