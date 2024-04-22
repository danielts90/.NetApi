using MarketPlaceDomain.Dto;

namespace MarketPlaceDomain.Entities
{
    public class User : EntityBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? LastLogin { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        protected override DtoBase ToDto() =>
            new UserDto
            {
                Id = Id,
                Email = Email,
                LastLogin = LastLogin,
                Name = Name
            };
    }
}
