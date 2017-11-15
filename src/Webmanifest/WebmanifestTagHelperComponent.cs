using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebEssentials.AspNetCore.Pwa
{
    internal class WebmanifestTagHelperComponent : TagHelperComponent
    {
        private const string _link = "\t<link rel=\"manifest\" href=\"" + Constants.WebManifestRoute + "\" />\r\n";
        private const string _themeFormat = "\t<meta name=\"theme-color\" content=\"{0}\" />\r\n";
        private PwaOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public WebmanifestTagHelperComponent(PwaOptions options, IServiceProvider serviceProvider)
        {
            _options = options;
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public override int Order => 100;

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_options.RegisterWebmanifest)
            {
                return;
            }

            if (!(_serviceProvider.GetService(typeof(WebManifest)) is WebManifest manifest))
            {
                return;
            }

            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(manifest.ThemeColor))
                {
                    output.PostContent.AppendHtml(string.Format(_themeFormat, manifest.ThemeColor));
                }

                output.PostContent.AppendHtml(_link);
            }
        }
    }
}
