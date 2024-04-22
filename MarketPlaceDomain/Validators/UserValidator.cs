using FluentValidation;
using MarketPlaceDomain.Dto;

namespace MarketPlaceDomain.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty().WithMessage("The field E-mail is required.")
                .EmailAddress().WithMessage("Invalid E-mail address.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull().WithMessage("The fiel Password is required")
                .MinimumLength(8).WithMessage("The Passwords needs 8 or more characthers");
        }
    }
}
