using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IShoppingCartItemRepository
    {
        List<ShoppingCartItem> GetAll();
        List<ShoppingCartItem> GetAllIncludeShoppingCartNav();
        ShoppingCartItem GetById(int Id);
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem Update(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Add(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem);

    }
}
