namespace Booking.Domain.Commands.Reservation
{
    public class CreateReservationCommand : Command, ICommand<CommandResult>
    {
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
