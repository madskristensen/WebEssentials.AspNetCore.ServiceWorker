using Microsoft.AspNetCore.Razor.TagHelpers;
using WebEssentials.AspNetCore.ServiceWorker;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> type.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ServiceWorker services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddServiceWorker(this IServiceCollection services)
        {
            services.AddTransient<ITagHelperComponent, ServiceWorkerTagHelperComponent>();
            services.AddTransient<ServiceWorkerOptions>();

            return services;
        }

        /// <summary>
        /// Adds ServiceWorker services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddServiceWorker(this IServiceCollection services, ServiceWorkerOptions options)
        {
            services.AddTransient<ITagHelperComponent, ServiceWorkerTagHelperComponent>();
            services.AddTransient(factory => options);

            return services;
        }
    }
}
