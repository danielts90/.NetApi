using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;

namespace MarketPlaceBusiness.Interfaces
{
    public interface IAutenticationService
    {
        string GenerateToken(UserDto user);
    }
}
