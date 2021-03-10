using FluentValidation;
using TestAPT.Models;

namespace TestAPT.Validators
{
    public class FileDetailValidator : AbstractValidator<FileDetail>
    {
        public FileDetailValidator()
        {
            RuleFor(d => d.Amount)
                .NotNull().WithMessage("[Amount] is a required field and cannot be null !")
                .GreaterThanOrEqualTo(1m).WithMessage("[Amount] must be greather than or equal to 1.00 No Less !")
                .LessThanOrEqualTo(20000000m).WithMessage("[Amount] cannot be greater that 20,000,000 !");
        }
    }
}
