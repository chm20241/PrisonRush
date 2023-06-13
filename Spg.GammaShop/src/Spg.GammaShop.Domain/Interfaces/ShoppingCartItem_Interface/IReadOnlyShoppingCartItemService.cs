using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IReadOnlyShoppingCartItemService
    {
        List<ShoppingCartItem> GetAll();
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem GetById(int Id);
        List<ShoppingCartItem> GetByShoppingCart(ShoppingCart shoppingCart);
    }
}
