using MarketPlaceDomain.Dto;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;

namespace MarketPlaceDomain.Services
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
