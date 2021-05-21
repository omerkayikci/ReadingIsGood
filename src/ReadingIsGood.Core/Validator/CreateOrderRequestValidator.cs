using FluentValidation;
using ReadingIsGood.Core.Request;

namespace ReadingIsGood.Core.Validator
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            this.RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer cannot be empty");

            this.RuleFor(x => x.OrderItems).SetValidator(new OrderItemModelValidator()).When(x => x.OrderItems != null);
        }
    }
}
