using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.UserInterfaces
{
    public interface IReadOnlyUserService
    {
        User? GetById(int Id);
        IEnumerable<User> GetAll();
        User? GetByGuid(Guid guid);
    }
}
