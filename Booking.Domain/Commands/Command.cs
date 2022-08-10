using FluentValidation.Results;

namespace Booking.Domain.Commands
{
    public abstract class Command
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
