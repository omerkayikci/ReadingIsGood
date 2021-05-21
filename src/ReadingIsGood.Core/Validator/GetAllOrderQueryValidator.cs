using FluentValidation;
using ReadingIsGood.Core.Query;

namespace ReadingIsGood.Core.Validator
{
    public class GetAllOrderQueryValidator : AbstractValidator<GetAllOrderQuery>
    {
        public GetAllOrderQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("customer cannot be null");
        }
    }
}
