using FluentValidation;
using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.Request;
using System;

namespace ReadingIsGood.Core.Validator
{
    public class OrderStatusUpdateRequestValidator : AbstractValidator<OrderStatusUpdateRequest>
    {
        public OrderStatusUpdateRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer name cannot be empty.");

            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId name cannot be empty.");

            Transform(from: x => x.OrderStatus, to: value => Enum.IsDefined(typeof(OrderStatus), value!) ? (OrderStatus?)Enum.Parse(typeof(OrderStatus), value!) : null)
                .NotNull().WithMessage("Incorrect Order status.")
                .When(x => string.IsNullOrEmpty(x.OrderStatus));
        }
    }
}
