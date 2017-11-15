using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    internal class WebManifestCache
    {
        private readonly IHostingEnvironment _env;
        private readonly MemoryCache _cache;
        private readonly string _fileName;

        public WebManifestCache(IHostingEnvironment env, string fileName)
        {
            _env = env;
            _fileName = fileName;
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public WebManifest GetManifest()
        {
            return _cache.GetOrCreate("webmanifest", (entry) =>
            {
                IFileInfo file = _env.WebRootFileProvider.GetFileInfo(_fileName);
                entry.AddExpirationToken(_env.WebRootFileProvider.Watch(_fileName));

                string json = File.ReadAllText(file.PhysicalPath);

                WebManifest manifest = JsonConvert.DeserializeObject<WebManifest>(json);
                manifest.FileName = _fileName;
                manifest.RawJson = Regex.Replace(json, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

                if (!manifest.IsValid(out string error))
                {
                    throw new JsonException(error);
                }

                return manifest;
            });
        }
    }
}
