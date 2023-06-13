using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.UserInterfaces
{
    public interface IAddUpdateableUserService
    {
        User? Add(User user);
        User? Update(Guid guid, User user);
    }
}
