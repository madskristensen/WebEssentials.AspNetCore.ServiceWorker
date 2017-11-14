using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    internal class WebManifestConfig : IConfigureOptions<WebManifest>
    {
        private IConfiguration _config;
        private IOptionsMonitorCache<WebManifest> _options;

        public WebManifestConfig(IConfiguration config, IOptionsMonitorCache<WebManifest> options)
        {
            _config = config;
            _options = options;
        }

        public void Configure(WebManifest options)
        {
            _config.GetReloadToken().RegisterChangeCallback(_ =>
            {
                _options.TryRemove(Options.DefaultName);
            }, null);

            ConfigurationBinder.Bind(_config.GetSection("webmanifest"), options);
        }
    }
}
