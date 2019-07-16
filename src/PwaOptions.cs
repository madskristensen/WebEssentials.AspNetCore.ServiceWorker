using System;
using Microsoft.Extensions.Configuration;

namespace WebEssentials.AspNetCore.Pwa
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
            BaseRoute = "";
            OfflineRoute = Constants.Offlineroute;
            RegisterServiceWorker = true;
            RegisterWebmanifest = true;
            EnableCspNonce = false;
            ServiceWorkerCacheControlMaxAge = 60 * 60 * 24 * 30;    // 30 days
            WebManifestCacheControlMaxAge = 60 * 60 * 24 * 30;      // 30 days
        }

        internal PwaOptions(IConfiguration config)
            : this()
        {
            CacheId = config["pwa:cacheId"] ?? CacheId;
            RoutesToPreCache = config["pwa:routesToPreCache"] ?? RoutesToPreCache;
            BaseRoute = config["pwa:baseRoute"] ?? BaseRoute;
            OfflineRoute = config["pwa:offlineRoute"] ?? OfflineRoute;

            if (bool.TryParse(config["pwa:registerServiceWorker"] ?? "true", out bool register))
            {
                RegisterServiceWorker = register;
            }

            if (bool.TryParse(config["pwa:registerWebmanifest"] ?? "true", out bool manifest))
            {
                RegisterWebmanifest = manifest;
            }

            if (bool.TryParse(config["pwa:EnableCspNonce"] ?? "true", out bool enableCspNonce))
            {
                EnableCspNonce = enableCspNonce;
            }

            if (Enum.TryParse(config["pwa:strategy"] ?? "cacheFirstSafe", true, out ServiceWorkerStrategy mode))
            {
                Strategy = mode;
            }

            if (int.TryParse(config["pwa:ServiceWorkerCacheControlMaxAge"], out int serviceWorkerCacheControlMaxAge))
            {
                ServiceWorkerCacheControlMaxAge = serviceWorkerCacheControlMaxAge;
            }

            if (int.TryParse(config["pwa:WebManifestCacheControlMaxAge"], out int webManifestCacheControlMaxAge))
            {
                WebManifestCacheControlMaxAge = webManifestCacheControlMaxAge;
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
        /// The base route to the application.
        /// </summary>
        public string BaseRoute { get; set; }

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

        /// <summary>
        /// Determines the value of the ServiceWorker CacheControl header Max-Age (in seconds)
        /// </summary>
        public int ServiceWorkerCacheControlMaxAge { get; set; }

        public int WebManifestCacheControlMaxAge { get; set; }

        /// <summary>
        /// Determines whether a CSP nonce will be added via NWebSec
        /// </summary>
        public bool EnableCspNonce { get; set; }

        /// <summary>
        /// Generate code even on HTTP connection. Necessary for SSL offloading.
        /// </summary>
        public bool AllowHttp { get; set; }
    }
}
