namespace Booking.Domain.Handlers
{
    public struct Messages
    {
        public const string NotFound = "Not found.";
        public const string AccessDenied = "Access denied.";
        public const string MaximumDaysInAdvance = "Your reservation cannot be done more than {0} days in advance.";
        public const string MaximumStayDuration = "Your stay cannont be longer than {0} days.";
        public const string ThereIsAnotherReservationSamePeriod = "There is already another reservation in place for the desired period.";
        public const string UnexpectedErrorSaveData = "An unexpected error occurred while saving the data.";
        public const string ReservationDeteleNotAllowed = "It is not possible to delete the reservation.";
        public const string ReservationUpdateNotAllowed = "It is not possible to update the reservation.";
    }
}
