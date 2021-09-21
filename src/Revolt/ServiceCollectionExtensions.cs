using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revolt.Options;

namespace Revolt
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRevolt(this IServiceCollection services)
        {
            services.AddOptions<RevoltOptions>()
                .BindConfiguration(RevoltOptions.Revolt);
            
            services.AddHttpClient<IRevoltClient, RevoltClient>();
            services.AddHttpClient<IPlatformClient, RevoltClient>();
            services.AddHttpClient<IAuthClient, RevoltClient>();
            services.AddHttpClient<IUsersClient, RevoltClient>();

            return services;
        }
    }
}