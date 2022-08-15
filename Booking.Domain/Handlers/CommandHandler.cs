using Booking.Domain.Interfaces;
using FluentValidation.Results;

namespace Booking.Domain.Handlers
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }
        
        protected void AddError(string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
        }

        protected ValidationResult ErrorResult(params string[] errors)
        {
            foreach (var error in errors)
                AddError(error);

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            if (await uow.CommitAsync() <= 0)
            {
                AddError(Messages.UnexpectedErrorSaveData);
            }

            return ValidationResult;
        }
    }
}
