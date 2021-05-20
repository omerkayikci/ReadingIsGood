using FluentValidation;
using ReadingIsGood.Core.Request;

namespace ReadingIsGood.Core.Validator
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            this.RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty.");

            this.RuleFor(x => x.Password).NotEmpty().WithMessage("Password name cannot be empty.");
        }
    }
}
