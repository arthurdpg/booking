using FluentValidation;

namespace Booking.Domain.Commands.Reservation.Validators
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.UserId)
                .MaximumLength(255).WithMessage(ValidationMessages.MaxLength);

            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.From)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.To)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.Observations)
                 .MaximumLength(500).WithMessage(ValidationMessages.MaxLength);

            // Date validations
        }
    }
}
