﻿using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IDeletableShoppingCartService
    {
        ShoppingCart Remove(ShoppingCart shoppingCart);
    }
}
