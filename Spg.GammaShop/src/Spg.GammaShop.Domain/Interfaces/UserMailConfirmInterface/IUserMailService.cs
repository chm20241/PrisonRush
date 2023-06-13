using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.UserMailConfirmInterface
{
    public interface IUserMailService
    {
        public UserMailConfirme SetUserMailConfirme(UserMailConfirme uMC);
    }
}
