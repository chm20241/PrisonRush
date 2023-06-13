using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.DTO
{
    public class ShoppingCartItemPostDTO
    {
        public int Pieces { get; set; }
        public Product? ProductNav { get; set; }
        public ShoppingCart? ShoppingCartNav { get; set; }
    }
}
