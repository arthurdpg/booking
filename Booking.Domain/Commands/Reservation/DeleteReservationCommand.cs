using Booking.Domain.Commands.Reservation.Validators;

namespace Booking.Domain.Commands.Reservation
{
    public class DeleteReservationCommand : Command, ICommand<CommandResult>
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
