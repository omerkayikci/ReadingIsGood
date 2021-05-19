using FluentValidation;
using ReadingIsGood.Core.Request;

namespace ReadingIsGood.Core.Validator
{
    public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>
    {
        public UpdateStockRequestValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id cannot be empty.");

            this.RuleFor(x => x.Stock)
                .NotNull().WithMessage("Stock cannot be null.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be 0 or less than 0");
        }
    }
}
