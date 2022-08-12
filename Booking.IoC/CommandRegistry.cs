using Booking.Bus;
using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Handlers.Reservation;
using Booking.Domain.Interfaces.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    public static class CommandRegistry
    {
        public static void RegisterCommand(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IRequestHandler<CreateReservationCommand, CommandResult>, CreateReservationHandler>();
            services.AddScoped<IRequestHandler<UpdateReservationCommand, CommandResult>, UpdateReservationHandler>();
            services.AddScoped<IRequestHandler<DeleteReservationCommand, CommandResult>, DeleteReservationHandler>();
        }
    }
}
