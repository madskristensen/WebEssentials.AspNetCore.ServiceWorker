namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// The various modes of service workers.
    /// </summary>
    public enum ServiceWorkerMode
    {
        /// <summary>
        /// Provides offline support, caches static resources with ?v=... querystring only, goes to network first for HTML
        /// </summary>
        Safe
    }
}
