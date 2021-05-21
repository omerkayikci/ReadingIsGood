using FluentValidation.Validators;
using ReadingIsGood.Core.Request;
using System;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Validator
{
    public class OrderItemModelValidator : PropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            foreach (OrderItemRequest orderItemRequest in (context.PropertyValue as List<OrderItemRequest>)!)
            {
                if (string.IsNullOrEmpty(orderItemRequest.ProductSku))
                {
                    SetErrorMessage("Sku cannot be null or empty");
                    return false;
                }

                if (string.IsNullOrEmpty(orderItemRequest.ProductId))
                {
                    SetErrorMessage("ProductId cannot be null or empty");
                    return false;
                }

                if (orderItemRequest.Quantity <= 0)
                {
                    SetErrorMessage("Quantity cannot be 0 or be less than 0");
                    return false;
                }

                if (orderItemRequest.TotalPrice != null && orderItemRequest.TotalPrice <= 0)
                {
                    SetErrorMessage("Total price cannot be null or be less than 0");
                    return false;
                }
            }

            return true;
        }
    }
}
