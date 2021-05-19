using FluentValidation;
using ReadingIsGood.Core.Request;

namespace ReadingIsGood.Core.Validator
{
    public class AddProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public AddProductRequestValidator()
        {
            this.RuleFor(x => x.SKU).NotEmpty().WithMessage("SKU cannot be empty.");

            this.RuleFor(x => x.Stock)
                .NotNull().WithMessage("Stock cannot be null.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be 0 or less than 0");
        }
    }
}
