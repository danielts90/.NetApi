using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;

namespace MarketPlaceBusiness.Interfaces
{
    public interface IUserService : IBaseService<UserDto, User>
    {
        public string AutenticateUser(UserDto user);
    }
}
