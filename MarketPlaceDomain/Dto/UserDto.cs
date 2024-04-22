using FluentValidation.Results;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Validators;

namespace MarketPlaceBusiness.Dto
{
    public class UserDto : DtoBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? LastLogin { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public override ValidationResult Validate()
        {
            var validator = new UserValidator();
            return validator.Validate(this);
        }

        protected override User ToEntity() =>
            new User
            {
                Id = Id,
                Name = Name,
                Email = Email,
                LastLogin = LastLogin,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
    }
}
