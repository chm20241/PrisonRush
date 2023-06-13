using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.Catagory_Interfaces
{
    public interface IAddUpdateableCatagoryService
    {
        Catagory AddCatagory(Catagory catagory);
        Catagory UpdateCatagory(int Id, Catagory catagory);
    }
}
