using FluentValidation;
using ReadingIsGood.Core.Query;

namespace ReadingIsGood.Core.Validator
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.customerId).NotEmpty().WithMessage("Invalid uid");
        }
    }
}
