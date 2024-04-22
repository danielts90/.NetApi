using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Helpers;
using MarketPlaceBusiness.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MarketPlaceBusiness.Autentication
{
    public class AutenticationService : IAutenticationService
    {
        private readonly AutenticationSettings _autenticationSettings;

        public AutenticationService(
             IOptions<AutenticationSettings> autenticationSettings)
        {
            _autenticationSettings = autenticationSettings.Value;
        }

        public string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_autenticationSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _autenticationSettings.Issuer,
                Audience = _autenticationSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_autenticationSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;

        }
    }
}
