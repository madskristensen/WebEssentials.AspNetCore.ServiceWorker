namespace WebEssentials.AspNetCore.Pwa
{
    /// <summary>
    /// The various modes of service workers.
    /// </summary>
    public enum ServiceWorkerStrategy
    {
        /// <summary>
        /// Serves all resources from cache and falls back to network.
        /// </summary>
        CacheFirst,

        /// <summary>
        /// Caches static resources with ?v=... querystring only. Checks network first for HTML.
        /// </summary>
        CacheFirstSafe,

        /// <summary>
        /// The minimal strategy does nothing and is good for when you only want a service worker in
        /// order for browsers to suggest installing your PWA.
        /// </summary>
        Minimal,

        /// <summary>
        /// Always tries the network first and falls back to cache when offline.
        /// </summary>
        NetworkFirst
    }
}
