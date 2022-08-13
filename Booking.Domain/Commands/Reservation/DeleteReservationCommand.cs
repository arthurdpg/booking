using Booking.Domain.Commands.Reservation.Validators;
using FluentValidation.Results;
using MediatR;

namespace Booking.Domain.Commands.Reservation
{
    public class DeleteReservationCommand : Command
    {
        public Guid ReservationId { get; private set; }

        public DeleteReservationCommand(Guid reservationId)
        {
            ReservationId = reservationId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteReservationValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
