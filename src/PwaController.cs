using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    /// <summary>
    /// A controller for manifest.webmanifest, serviceworker.js and offline.html
    /// </summary>
    public class PwaController : Controller
    {
        private PwaOptions _options;

        /// <summary>
        /// Creates an instance of the controller.
        /// </summary>
        public PwaController(PwaOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Serves a service worker based on the provided settings.
        /// </summary>
        [Route(Constants.ServiceworkerRoute)]
        public async Task<IActionResult> ServiceWorkerAsync()
        {
            Response.ContentType = "application/javascript; charset=utf-8";
            Response.Headers[HeaderNames.CacheControl] = "max-age=0";

            string fileName = _options.Strategy + ".js";
            Assembly assembly = typeof(PwaController).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream($"WebEssentials.AspNetCore.ServiceWorker.ServiceWorker.Files.{fileName}");

            using (var reader = new StreamReader(resourceStream))
            {
                string js = await reader.ReadToEndAsync();
                string modified = js
                    .Replace("{version}", _options.CacheId + "::" + _options.Strategy)
                    .Replace("{routes}", string.Join(",", _options.RoutesToPreCache.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "'" + r.Trim() + "'")))
                    .Replace("{offlineRoute}", _options.OfflineRoute);

                return Content(modified);
            }
        }

        /// <summary>
        /// Serves the offline.html file
        /// </summary>
        [Route(Constants.Offlineroute)]
        public async Task<IActionResult> OfflineAsync()
        {
            Response.ContentType = "text/html";

            Assembly assembly = typeof(PwaController).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream("WebEssentials.AspNetCore.ServiceWorker.ServiceWorker.Files.offline.html");

            using (var reader = new StreamReader(resourceStream))
            {
                return Content(await reader.ReadToEndAsync());
            }
        }

        /// <summary>
        /// Serves the offline.html file
        /// </summary>
        [Route(Constants.WebManifestRoute)]
        public IActionResult WebManifest([FromServices] WebManifest wm)
        {
            if (wm == null)
            {
                return NotFound();
            }

            Response.ContentType = "application/manifest+json; charset=utf-8";

            return Content(wm.RawJson);
        }
    }
}
