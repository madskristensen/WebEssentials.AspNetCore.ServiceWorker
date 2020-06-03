using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Semantics;
using Microsoft.Net.Http.Headers;

namespace WebEssentials.AspNetCore.Pwa
{
    /// <summary>
    /// A controller for manifest.webmanifest, serviceworker.js and offline.html
    /// </summary>
    public class PwaController : Controller
    {
        private readonly PwaOptions _options;
        private readonly RetrieveCustomServiceworker _customServiceworker;

        /// <summary>
        /// Creates an instance of the controller.
        /// </summary>
        public PwaController(PwaOptions options, RetrieveCustomServiceworker customServiceworker)
        {
            _options = options;
            _customServiceworker = customServiceworker;
        }

        /// <summary>
        /// Serves a service worker based on the provided settings.
        /// </summary>
        [Route(Constants.ServiceworkerRoute)]
        [HttpGet]
        public async Task<IActionResult> ServiceWorkerAsync()
        {
            Response.ContentType = "application/javascript; charset=utf-8";
            Response.Headers[HeaderNames.CacheControl] = $"max-age={_options.ServiceWorkerCacheControlMaxAge}";

            if (_options.Strategy == ServiceWorkerStrategy.CustomStrategy)
            {
                string js = _customServiceworker.GetCustomServiceworker(_options.CustomServiceWorkerStrategyFileName);
                return Content(InsertStrategyOptions(js)); 
            }

            else
            {
                string fileName = _options.Strategy + ".js";
                Assembly assembly = typeof(PwaController).Assembly;
                Stream resourceStream = assembly.GetManifestResourceStream($"WebEssentials.AspNetCore.Pwa.ServiceWorker.Files.{fileName}");

                using (var reader = new StreamReader(resourceStream))
                {
                    string js = await reader.ReadToEndAsync();
                    return Content(InsertStrategyOptions(js));
                }
            }
        }

        private string InsertStrategyOptions(string javascriptString)
        {
            return javascriptString
                .Replace("{version}", _options.CacheId + "::" + _options.Strategy)
                .Replace("{routes}", string.Join(",", _options.RoutesToPreCache.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "'" + r.Trim() + "'")))
                .Replace("{offlineRoute}", _options.BaseRoute + _options.OfflineRoute)
                .Replace("{ignoreRoutes}", string.Join(",", _options.RoutesToIgnore.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "'" + r.Trim() + "'")));
        }

        /// <summary>
        /// Serves the offline.html file
        /// </summary>
        [Route(Constants.Offlineroute)]
        [HttpGet]
        public async Task<IActionResult> OfflineAsync()
        {
            Response.ContentType = "text/html";

            Assembly assembly = typeof(PwaController).Assembly;
            Stream resourceStream = assembly.GetManifestResourceStream("WebEssentials.AspNetCore.Pwa.ServiceWorker.Files.offline.html");

            using (var reader = new StreamReader(resourceStream))
            {
                return Content(await reader.ReadToEndAsync());
            }
        }

        /// <summary>
        /// Serves the manifest.json file
        /// </summary>
        [Route(Constants.WebManifestRoute)]
        [HttpGet]
        public IActionResult WebManifest([FromServices] WebManifest wm)
        {
            if (wm == null)
            {
                return NotFound();
            }

            Response.ContentType = "application/manifest+json; charset=utf-8";


            Response.Headers[HeaderNames.CacheControl] = $"max-age={_options.WebManifestCacheControlMaxAge}";

            return Content(wm.RawJson);
        }
    }
}
