using FluentValidation;

namespace Booking.Domain.Commands.Reservation.Validators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.UserId)
                .MaximumLength(255).WithMessage(ValidationMessages.MaxLength);

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.From)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.To)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.Observations)
                 .MaximumLength(500).WithMessage(ValidationMessages.MaxLength);

            RuleFor(x => x.From)
                .Must(x => x.Date > DateTime.Now.Date).WithMessage(ValidationMessages.InvalidValue);

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From).WithMessage(ValidationMessages.InvalidValue);
        }
    }
}
