using Booking.Domain.Commands.Reservation.Validators;
using FluentValidation.Results;

namespace Booking.Domain.Commands.Reservation
{
    public class DeleteReservationCommand : Command
    {
        public Guid UserId { get; private set; }
        public Guid ReservationId { get; private set; }

        public DeleteReservationCommand(Guid userId, Guid reservationId)
        {
            UserId = userId;
            ReservationId = reservationId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteReservationValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
