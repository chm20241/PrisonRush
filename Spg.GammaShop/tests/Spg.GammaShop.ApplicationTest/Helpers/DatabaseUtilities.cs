using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Spg.AutoTeileShop.

namespace Spg.GammaShop.ApplicationTest.Helpers
{
    public static class DatabaseUtilities
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
            var cat1 = new Catagory(null, "Proteinshake", "Test Description", CategoryTypes.Shake);

            return new List<Catagory>()
            {   cat1,
                new Catagory(cat1, "EAA", "Test Description2", CategoryTypes.Tabletten),
            };
        }



        public static List<User> GetSeedingUser()
        {
            return new List<User>()
            {

                new User
                ( new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"),
                "Max", "Mustermann", "Musterstr. 1", "0123456789",
                "max.mustermann@example.com", "password123", Roles.Admin, true),

                new User
                ( new Guid("gu5lda13-kfh5-8574-kc0e-40a2a62dd963"),
                "Anna", "Schmidt", "Hauptstr. 42", "0987654321",
                "anna.schmidt@example.com", "password456", Roles.User, false)
            };
        }



        public static List<ShoppingCartItem> GetSeedingShoppingCartItems(GammaShopContext db)
        {
            return new List<ShoppingCartItem>()
            {
                new ShoppingCartItem(                
                    new Guid("da7de159-0d7d-4416-b7e0-9c6723fd333f"),
                    2,
                    db.Products.First().Id,
                    db.Products.First(),
                    null,
                    null
                ),
                
                 new ShoppingCartItem(
                    new Guid("5b4c4bbf-e458-46a7-8827-d02b934cba78"),
                    2,
                    db.Products.First().Id,
                    db.Products.First(),
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
                    new Guid("bb9b715c-a3f6-4246-84f9-1a1bd6beb211"),
                    db.Users.First().Id,
                    db.Users.First(),
                    db.ShoppingCartItems.ToList()
                    ),
                new ShoppingCart(
                    new Guid("47529ac2-9b86-4405-9e5e-5bcf55dc4bb3"),
                    db.Users.Last().Id,
                    db.Users.Last(),
                    new List<ShoppingCartItem>{db.ShoppingCartItems.First()}
                    )
            };
        }

        private static List<UserMailConfirme> GetSeedingUserMailConfirme(GammaShopContext db)
        {
            return new List<UserMailConfirme>()
            {
                new UserMailConfirme(
                    db.Users.First(),
                     ComputeSha256Hash(Guid.NewGuid().ToString().Substring(0, 8)),
                     DateTime.Now
                    ),
                 new UserMailConfirme(
                    db.Users.Last(),
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
