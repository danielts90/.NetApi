using MarketPlaceBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceBusiness.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User? Autenticate(User user);
    }
}
