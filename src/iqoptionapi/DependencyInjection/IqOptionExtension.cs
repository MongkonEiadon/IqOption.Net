using Microsoft.Extensions.DependencyInjection;

namespace iqoptionapi.DependencyInjection {
    public static class IqOptionExtension {
        public static IServiceCollection AddIqOptionApi(this IServiceCollection services,
            IqOptionConfiguration configuration) {
            services.Configure<IqOptionConfiguration>(c => { });
            services.AddTransient<IIqOptionApi, IqOptionApi>();


            return services;
        }
    }
}