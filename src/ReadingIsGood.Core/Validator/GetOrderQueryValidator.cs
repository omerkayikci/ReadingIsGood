using FluentValidation;
using ReadingIsGood.Core.Query;

namespace ReadingIsGood.Core.Validator
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer cannot be empty");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId cannot be empty");
        }
    }
}
