using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceBusiness.Services
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private IUserRepository _userRepository;
        private readonly IAutenticationService _authenticationService;
        public UserService(IUserRepository userRepository, 
                           IAutenticationService authenticationService) : base(userRepository)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public string AutenticateUser(UserDto user)
        {
            var autenticatedUser =  _userRepository.Autenticate((User)user);
            if (autenticatedUser != null)
            {
                return _authenticationService.GenerateToken((UserDto)autenticatedUser);
            }
            else
                throw (new Exception("An error ocurred while process login operation, please check your parameters."));
            
        }
    }
}
