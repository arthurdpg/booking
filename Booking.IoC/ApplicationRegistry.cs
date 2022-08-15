using Booking.Application.Interfaces;
using Booking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    public static class ApplicationRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomAppService, RoomAppService>();
            services.AddScoped<IReservationAppService, ReservationAppService>();
        }
    }
}
