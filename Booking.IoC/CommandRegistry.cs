using Booking.Bus;
using Booking.Domain.Interfaces.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    public static class CommandRegistry
    {
        public static void RegisterCommand(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
        }
    }
}
