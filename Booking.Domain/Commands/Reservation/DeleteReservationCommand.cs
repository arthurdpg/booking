namespace Booking.Domain.Commands.Reservation
{
    public class DeleteReservationCommand : Command, ICommand<CommandResult>
    {
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
