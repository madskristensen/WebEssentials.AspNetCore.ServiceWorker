using Microsoft.Extensions.Configuration;
using System;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// Options for the service worker.
    /// </summary>
    public class PwaOptions
    {
        /// <summary>
        /// Creates a new default instance of the options.
        /// </summary>
        public PwaOptions()
        {
            CacheId = Constants.DefaultCacheId;
            Strategy = ServiceWorkerStrategy.CacheFirstSafe;
            RoutesToPreCache = "";
            OfflineRoute = Constants.Offlineroute;
            RegisterServiceWorker = true;
            RegisterWebmanifest = true;
        }

        internal PwaOptions(IConfiguration config)
            : this()
        {
            CacheId = config["serviceworker:cacheId"] ?? CacheId;
            RoutesToPreCache = config["serviceworker:routesToPreCache"] ?? RoutesToPreCache;
            OfflineRoute = config["serviceworker:offlineRoute"] ?? OfflineRoute;

            if (bool.TryParse(config["serviceworker:registerServiceWorker"] ?? "true", out bool register))
            {
                RegisterServiceWorker = register;
            }

            if (bool.TryParse(config["webmanifest:registerWebmanifest"] ?? "true", out bool manifest))
            {
                RegisterWebmanifest = manifest;
            }

            if (Enum.TryParse(config["serviceworker:strategy"] ?? "cacheFirstSafe", true, out ServiceWorkerStrategy mode))
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
        /// The route to the page to show when offline.
        /// </summary>
        public string OfflineRoute { get; set; }

        /// <summary>
        /// Determines if a script that registers the service worker should be injected
        /// into the bottom of the HTML page.
        /// </summary>
        public bool RegisterServiceWorker { get; set; }

        /// <summary>
        /// Determines if a meta tag that points to the web manifest should be inserted
        /// at the end of the head element.
        /// </summary>
        public bool RegisterWebmanifest { get; set; }
    }
}
