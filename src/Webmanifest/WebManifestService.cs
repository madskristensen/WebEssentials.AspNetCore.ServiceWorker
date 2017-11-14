using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    internal class WebManifestService : IWebManifestService
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public WebManifestService(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public WebManifest GetManifest()
        {
            IConfigurationSection section = _config.GetSection("webmanifest");

            if (section.Exists())
            {
                return section.Get<WebManifest>();
            }

            IFileInfo manifest = _env.WebRootFileProvider.GetFileInfo("manifest.webmanifest");

            if (!manifest.Exists)
            {
                manifest = _env.WebRootFileProvider.GetFileInfo("manifest.json");
            }

            if (manifest.Exists)
            {
                return JsonConvert.DeserializeObject<WebManifest>(File.ReadAllText(manifest.PhysicalPath));
            }

            return null;
        }
    }
}
