using FluentValidation;
using ReadingIsGood.Core.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Validator
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.productId).NotEmpty().WithMessage("Invalid product id.");
        }
    }
}
