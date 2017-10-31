using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// A controller for serviceworker.js and offline.html
    /// </summary>
    public class ServiceWorkerController : Controller
    {
        private ServiceWorkerOptions _options;

        /// <summary>
        /// Creates an instance of the controller.
        /// </summary>
        public ServiceWorkerController(ServiceWorkerOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Serves a service worker based on the provided settings.
        /// </summary>
        [Route("/serviceworker.js")]
        public async Task<IActionResult> ServiceWorkerAsync()
        {
            Response.ContentType = "application/javascript";

            string fileName = _options.Mode.ToString().ToLowerInvariant() + ".js";
            Assembly assembly = typeof(ServiceWorkerController).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream($"WebEssentials.AspNetCore.ServiceWorker.ServiceWorkers.{fileName}");

            using (var reader = new StreamReader(resourceStream))
            {
                string js = await reader.ReadToEndAsync();
                string modified = js
                    .Replace("{version}", _options.Version)
                    .Replace("{routes}", string.Join(",", _options.RoutesToPreCache.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "'" + r.Trim() + "'" )));

                return Content(modified);
            }
        }

        /// <summary>
        /// Serves the offline.html file
        /// </summary>
        [Route("/offline.html")]
        public async Task<IActionResult> OfflineAsync()
        {
            Response.ContentType = "text/html";

            Assembly assembly = typeof(ServiceWorkerController).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream("WebEssentials.AspNetCore.ServiceWorker.ServiceWorkers.offline.html");

            using (var reader = new StreamReader(resourceStream))
            {
                return Content(await reader.ReadToEndAsync());
            }
        }
    }
}
