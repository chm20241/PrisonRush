using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.ApplicationTest.Helpers
{
    public class MockUtilities
    {


        public static Product GetSeedingProduct()
        {
            return new Product(1, Guid.NewGuid(), "test", 50.0m, GetSeedingCatagory_withoutTopCat(), "test",
                "test", Rating.Gut, 2, 0, DateTime.Now);
        }

        public static List<Product> GetSeedingProductsList()
        {
            return new List<Product>()
            {
                new Product(1, Guid.NewGuid(), "test", 50.0m, GetSeedingCatagory_withoutTopCat(), "test",
                "test", Rating.Gut, 2, 0, DateTime.Now),

                new Product(2, Guid.NewGuid(), "test2", 50.0m, GetSeedingCatagory_withoutTopCat(), "test2",
                "test", Rating.Gut, 2, 0, DateTime.Now),

            };

        }
        public static Catagory GetSeedingCatagory_withoutTopCat()
        {
            return new Catagory()
            {
                Name = "Test",
                Description = "Test",
                CategoryType = CategoryTypes.Shake,
            };
        }

        public static Catagory GetSeedingCatagory_with_TopCat()
        {
            Catagory c = new Catagory()
            {
                Name = "Test",
                Description = "Test",
                CategoryType = CategoryTypes.Tabletten,
            };

            return new Catagory()
            {
                Name = "Test2",
                Description = "Test2",
                CategoryType = CategoryTypes.Drops,
                TopCatagory = c
            };
        }

        public static User GetSeedingUser()
        {
            return new User() {
                Guid = Guid.NewGuid(),
                Nachname= "Test",
                Addrese = "Test",
                Email = "Test",
                PW = "Test",
                Vorname = "Test",
                Telefon = "Test",
                Confirmed = false,
                Role = Roles.User
            };
        }

        public static ShoppingCartItem GetSeedingShoppingCartItem()
        {
            ShoppingCart sc = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                UserNav = GetSeedingUser()
            };

            return new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 2,
                ProductNav = GetSeedingProduct(),
                ShoppingCartNav = sc
            };
        }

        public static ShoppingCart GetSeedingShoppingCart() 
        {
            ShoppingCart sc = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                UserNav = GetSeedingUser()
            };
            sc.AddShoppingCartItem(GetSeedingShoppingCartItem());
            return sc;
        }
    }
}
