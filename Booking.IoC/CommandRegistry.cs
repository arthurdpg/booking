using Booking.Bus;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Handlers.Reservation;
using Booking.Domain.Interfaces.Bus;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    public static class CommandRegistry
    {
        public static void RegisterCommand(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IRequestHandler<CreateReservationCommand, ValidationResult>, CreateReservationHandler>();
            services.AddScoped<IRequestHandler<UpdateReservationCommand, ValidationResult>, UpdateReservationHandler>();
            services.AddScoped<IRequestHandler<DeleteReservationCommand, ValidationResult>, DeleteReservationHandler>();
        }
    }
}
