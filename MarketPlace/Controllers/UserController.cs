using MarketPlace.Helpers;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class UserController : BaseController<UserDto, User>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Autenticate")]
        public IActionResult Autenticate(UserDto user)
        {
            try
            {
                var token = _userService.AutenticateUser(user);
                return Ok(new CustomResponse<string>(true, "Autenticated", token));
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomResponse<string>(false, "Autentication Failed", ex.Message));
                
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateRoot")]
        public IActionResult CreateRootUser()
        {
            var result = _userService.Insert(new UserDto
            {
                Name = "root",
                Email = "root@teste.com",
                Password = "teste@123",
                ConfirmPassword = "teste@123"
            });

            return Ok(new CustomResponse<int>(true, "Usuer created",  result));
        }
    }
}
