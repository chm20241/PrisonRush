﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Infrastructure
{
    public class GammaShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<UserMailConfirme> UserMailConfirms { get; set; }


        public GammaShopContext(DbContextOptions options) : base(options)
        {
        }

        protected GammaShopContext() : this(new DbContextOptions<DbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                //options.UseSqlite("DataSource= I:\\Dokumente 4TB\\HTL\\4 Klasse\\POS1 Git Repo\\sj2223-4bhif-pos-rest-api-project-autoteileshop\\Spg.AutoTeileShop\\src\\Spg.AutoTeileShop.API\\dbAutoTeileShop.db"); //Home PC
                // options.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db"); //Home PC
            options.UseLazyLoadingProxies();                                                                                                                                    
            options.UseSqlite(ReadLineWithQuestionMark());
            //options.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db");
            

            //  D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db"     //Laptop


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1017);

            List<User> users = new Faker<User>("de")
                .Rules((f, c) =>
                {
                    c.Guid = f.Random.Guid();
                    c.Vorname = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
                    c.Nachname = f.Name.LastName();
                    c.Email = f.Internet.Email(c.Vorname, c.Nachname);
                    c.Telefon = f.Person.Phone;
                    c.Addrese = f.Address.FullAddress();
                    c.Role = f.Random.Enum<Roles>();
                    c.Salt = GenerateSalt();
                    c.PW = CalculateHash(f.Internet.Password(), c.Salt);

                })
            .Generate(50)
            .ToList();
            Users.AddRange(users);
            SaveChanges();


            Random random = new Random();

            List<Catagory> catagory = new Faker<Catagory>("de")
            .Rules((f, ca) =>
            {

                ca.Description = f.Name.JobDescriptor();
                Array values = Enum.GetValues(typeof(CategoryTypes));                              
                ca.CategoryType = (CategoryTypes)values.GetValue(random.Next(values.Length));
                ca.Name = ca.CategoryType.ToString();
                
            })
            .Generate(20)
            .ToList();
            


            foreach (Catagory c in catagory)
            {
                if (c.CategoryType == CategoryTypes.Shake || c.CategoryType == CategoryTypes.Pulver || c.CategoryType == CategoryTypes.Drops || c.CategoryType == CategoryTypes.Bar || c.CategoryType == CategoryTypes.Tabletten)
                {
                    c.TopCatagory = null;
                }
                else
                {
                    while (c.TopCatagory == null)
                    {
                        Catagory topCatagory = catagory[random.Next(catagory.Count)];
                        if (topCatagory.CategoryType == CategoryTypes.Shake || topCatagory.CategoryType == CategoryTypes.Pulver || topCatagory.CategoryType == CategoryTypes.Drops || topCatagory.CategoryType == CategoryTypes.Bar || topCatagory.CategoryType == CategoryTypes.Tabletten)
                        {
                            c.TopCatagory = topCatagory;
                        }
                    }
                }
            }

            Catagories.AddRange(catagory);
            SaveChanges();

            
            List<Product> product = new Faker<Product>("de")
            .Rules((f, p) =>
            {
                
                p.Guid = f.Random.Guid();
                p.Name = f.Commerce.ProductName();
                p.Price = f.Random.Decimal(0, 1000);
                p.catagory = f.PickRandom(catagory);
                p.Description = f.Commerce.ProductDescription();
                p.Image = f.Image.PicsumUrl();

                Array values = Enum.GetValues(typeof(Rating));
                Random random = new Random();
                p.Rating = (Rating)values.GetValue(random.Next(values.Length));

                p.Stock = f.Random.Int(11, 1000);
                p.Discount = f.Random.Int(0, 100);
                p.receive = f.Date.Past(10, DateTime.Now);
                

            })
            .Generate(500)
            .ToList();
            product.AddRange(product);
            SaveChanges();

            List<ShoppingCartItem> shoppingCartItems = new Faker<ShoppingCartItem>("de")
                   .Rules((f, shI) =>
                   {
                       shI.guid = f.Random.Guid();
                       shI.Pieces = f.Random.Int(1, 9);
                       shI.ProductNav = f.PickRandom(product);
                   })
                   .Generate(100)
                   .ToList();
            ShoppingCartItems.AddRange(shoppingCartItems);
            SaveChanges();

            List<ShoppingCartItem> items2 = new();
            items2.AddRange(shoppingCartItems);

            List<ShoppingCart> shoppingCart = new Faker<ShoppingCart>("de")
           .Rules((f, sh) =>
           {
               sh.guid = f.Random.Guid();
               sh.UserNav = f.PickRandom(users);
               for(int i = 0; i < 2; i++)
               {
                   var item = f.PickRandom(items2);
                   sh.AddShoppingCartItem(item);
                   items2.Remove(item);
               }
           })
           .Generate(50)
           .ToList();
            ShoppingCarts.AddRange(shoppingCart);
            SaveChanges();


            List<UserMailConfirme> userMailConfirmes = new Faker<UserMailConfirme>("de")
           .Rules((f, u) =>
           {
               u.User = f.PickRandom(users);
               u.Code = ComputeSha256Hash(Guid.NewGuid().ToString().Substring(0, 8));
               //u.UserId = u.User.Id;
           })
           .Generate(50)
           .ToList();
            UserMailConfirms.AddRange(userMailConfirmes);
            SaveChanges();

        }


        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public string GenerateSalt()
        {
            // 128bit Salt erzeugen.
            byte[] salt = new byte[128 / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string ReadLineWithQuestionMark()
        {
            string relativeFilePath = "DataSource.txt";
            string currentDirectory = Environment.CurrentDirectory;
            int endIndex;
            string extractedPath;
            string filePath;
            if (currentDirectory.Contains($"\\src\\"))
            {
                endIndex = currentDirectory.IndexOf($"\\src\\") + $"\\src\\".Length;
                extractedPath = currentDirectory.Substring(0, endIndex);
                filePath = Path.Combine(extractedPath, relativeFilePath);
            }
            else
            {
                endIndex = currentDirectory.IndexOf($"\\tests\\");
                extractedPath = currentDirectory.Substring(0, endIndex);
                //filePath = Path.Combine(extractedPath, $"\\src\\", relativeFilePath);
                filePath = extractedPath + $"\\src\\" + relativeFilePath;
            }


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Die Datei wurde nicht gefunden.", relativeFilePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.TrimStart().StartsWith("?"))
                {
                    return line.TrimStart('?').Trim();
                }
            }

            return null;
        }
        public string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash = new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird Base64 codiert zurückgegeben.
            string hashedPassword = Convert.ToBase64String(hashedData);
            Console.WriteLine($"Salt:            {salt}");
            Console.WriteLine($"Password:        {password}");
            Console.WriteLine($"Hashed Password: {hashedPassword}");
            return hashedPassword;
        }
    }
}
