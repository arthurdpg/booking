using Booking.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var rulesConfig = configuration.GetSection("Rules").Get<RulesConfig>();
            if (rulesConfig != null)
                services.AddSingleton(rulesConfig);

            services.RegisterCommand();
            services.RegisterDatabase(configuration);
        }

        public static void ApplyIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplyDefaultIdentity(configuration);
        }

        public static void ApplyMigrations(this IServiceProvider services)
        {
            services.ApplyDatabaseMigrations();
        }
    }
}
