using MarketPlaceDomain.Dto;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class UserController : BaseController<UserDto, User>
    {
        public UserController(IUserService userService) : base(userService)
        {
        }
    }
}
