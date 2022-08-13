using Booking.Domain.Commands.Reservation.Validators;

namespace Booking.Domain.Commands.Reservation
{
    public class UpdateReservationCommand : Command
    {
        public string UserId { get; private set; }
        public Guid ReservationId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string Observations { get; private set; }

        public UpdateReservationCommand(string userId, Guid reservationId, DateTime from, DateTime to, string observations)
        {
            UserId = userId;
            ReservationId = reservationId;
            From = from;
            To = to;
            Observations = observations;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateReservationValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
