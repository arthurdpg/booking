using Booking.Domain.Commands.Reservation.Validators;
using FluentValidation.Results;
using MediatR;

namespace Booking.Domain.Commands.Reservation
{
    public class DeleteReservationCommand : Command
    {
        public string UserId { get; private set; }
        public Guid ReservationId { get; private set; }

        public DeleteReservationCommand(string userId, Guid reservationId)
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
