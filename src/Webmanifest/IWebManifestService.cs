namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// A service for querying the web app manifest.
    /// </summary>
    public interface IWebManifestService
    {
        /// <summary>
        /// Gets the manifest from either configuration or file system.
        /// </summary>
        WebManifest GetManifest();
    }
}