using FluentValidation;
using ReadingIsGood.Common.Utils;
using ReadingIsGood.Core.Request;

namespace ReadingIsGood.Core.Validator
{
    public class AddCustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public AddCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Customer name cannot be empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Customer phone number cannot be empty.")
                .Must(RegexHelper.IsValidPhoneNumber).WithMessage("Customer phone number incorrect format.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Customer email number cannot be empty.")
                .Must(RegexHelper.IsValidEmail).WithMessage("Customer Email incorrect format.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Customer address cannot be empty.");
        }
    }
}
