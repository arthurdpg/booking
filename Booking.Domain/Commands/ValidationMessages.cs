namespace Booking.Domain.Commands
{
    public struct ValidationMessages
    {
        public const string Required = "The {PropertyName} field is required.";
        public const string MaxLength = "The {PropertyName} maxlength is {MaxLength} characters.";
        public const string InvalidValue = "The {PropertyName} has an invalid value.";
    }
}
