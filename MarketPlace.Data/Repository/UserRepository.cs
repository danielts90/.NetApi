using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceData.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User? Autenticate(User user)
        {
            var autenticatedUser = base.entities.FirstOrDefault(u => u.Email.Trim().ToUpper().Equals(user.Email.Trim().ToUpper()) && u.Password == user.Password);
            return autenticatedUser;
        }
    }
}
