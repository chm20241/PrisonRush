using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.RepositoryTest.Helpers
{
    public class DatabaseUtilities
    {
        public static DbContextOptions GetDbOptions()
        {
            // Das garantiert eine DB-Connection die von EF Core nicht automatisch geschlossen wird
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();



            return new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;
        }



        public static void InitializeDatabase(GammaShopContext db)
        {
            db.Database.EnsureCreated();

            db.Catagories.AddRange(GetSeedingCategories());
            db.SaveChanges();

            db.Users.AddRange(GetSeedingUser());
            db.SaveChanges();

            db.ShoppingCartItems.AddRange(GetSeedingShoppingCartItems(db));
            db.SaveChanges();


            db.ShoppingCarts.AddRange(GetSeedingShoppingCart(db));
            db.SaveChanges();

            db.UserMailConfirms.AddRange(GetSeedingUserMailConfirme(db));
            db.SaveChanges();

        }



        public static List<Catagory> GetSeedingCategories()
        {
            var cat1 = new Catagory(null, "MotorKopf", "Test Description", CategoryTypes.Bar);

            return new List<Catagory>()
            {   cat1,
                new Catagory(cat1, "Zylinder", "Test Description2", CategoryTypes.Bar),
            };
        }



        public static List<User> GetSeedingUser()
        {
            return new List<User>()
            {

                new User
                ( Guid.NewGuid(),
                "Max", "Mustermann", "Musterstr. 1", "0123456789",
                "max.mustermann@example.com", "password123", Roles.Admin, true),

                new User
                ( Guid.NewGuid(),
                "Anna", "Schmidt", "Hauptstr. 42", "0987654321",
                "anna.schmidt@example.com", "password456", Roles.User, false)
            };
        }

        public static List<ShoppingCartItem> GetSeedingShoppingCartItems(GammaShopContext db)
        {
            return new List<ShoppingCartItem>()
            {
                new ShoppingCartItem(
                    Guid.NewGuid(),
                    2,
                    db.Products.OrderBy(u => u.Id).First().Id,
                    db.Products.OrderBy(u => u.Id).First(),
                    null,
                    null
                ),

                 new ShoppingCartItem(
                    Guid.NewGuid(),
                    2,
                    db.Products.OrderBy(u => u.Id).First().Id,
                    db.Products.OrderBy(u => u.Id).First(),
                    null,
                    null
                )

            };
        }

        public static List<ShoppingCart> GetSeedingShoppingCart(GammaShopContext db)
        {
            return new List<ShoppingCart>()
            {
                new ShoppingCart(
                    Guid.NewGuid(),
                    db.Users.OrderBy(u => u.Id).First().Id,
                    db.Users.OrderBy(u => u.Id).First(),
                    db.ShoppingCartItems.ToList()
                    ),
                new ShoppingCart(
                    Guid.NewGuid(),
                    db.Users.OrderBy(u => u.Id).Last().Id,
                    db.Users.OrderBy(u => u.Id).Last(),
                    new List<ShoppingCartItem>{db.ShoppingCartItems.First()}
                    )
            };
        }

        public static List<UserMailConfirme> GetSeedingUserMailConfirme(GammaShopContext db)
        {
            return new List<UserMailConfirme>()
            {
                new UserMailConfirme(
                    db.Users.OrderBy(u => u.Id).First(),
                     ComputeSha256Hash(Guid.NewGuid().ToString().Substring(0, 8)),
                     DateTime.Now
                    ),
                 new UserMailConfirme(
                    db.Users.OrderBy(u => u.Id).Last(),
                     ComputeSha256Hash(Guid.NewGuid().ToString().Substring(0, 8)),
                     DateTime.Now.AddDays(-2)
                    )
            };
        }


        public static string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
