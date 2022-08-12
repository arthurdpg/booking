using Booking.Domain.Commands.Reservation.Validators;

namespace Booking.Domain.Commands.Reservation
{
    public class CreateReservationCommand : Command, ICommand<CommandResult>
    {
        public string UserId { get; private set; }
        public Guid RoomId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string Observations { get; private set; }

        public CreateReservationCommand(string userId, Guid roomId, DateTime from, DateTime to, string observations)
        {
            UserId = userId;
            RoomId = roomId;
            From = from;
            To = to;
            Observations = observations;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateReservationValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
