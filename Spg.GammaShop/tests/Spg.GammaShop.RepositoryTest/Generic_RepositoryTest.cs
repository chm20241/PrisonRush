using Microsoft.EntityFrameworkCore;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.RepositoryTest
{
    public class Generic_RepositoryTest
    {
        private GammaShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                    .UseSqlite(ReadLineWithQuestionMark())
                 .UseSqlite(@"Data Source =3 C:\Users\bernd\Documents\Spg.GammaShop")  
                .Options;

            GammaShopContext db = new GammaShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
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


        [Fact]
        public async Task GetQueryable_User_ReturnsExpectedResult()
        {
            // Arrange
            GammaShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            IQueryable<User> queryable = await repo.GetQueryable(
                filter: null,
                sortOrder: null,
                includeNavigationProperty: "",
                skip: null,
                take: null
            );

            // Assert
            List<User> resultList = queryable.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(user1, resultList);
            Assert.Contains(user2, resultList);
        }

        [Fact]
        public void GetById_User_ReturnsExpectedResult()
        {
            // Arrange
            GammaShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            User result1 = repo.GetById(user1.Id);
            User result2 = repo.GetById(user2.Id);
            User result3 = repo.GetById(3);

            // Assert
            Assert.Equal(user1, result1);
            Assert.Equal(user2, result2);
            Assert.Null(result3);
        }

        [Fact]
        public void GetSingleOrDefaultByGuid_User_ReturnsExpectedResult()
        {
            // Arrange
            GammaShopContext db = createDB();
            ReadOnlyRepositoryBase<User> repo = new ReadOnlyRepositoryBase<User>(db);
            User user1 = new User(Guid.NewGuid(), "John", "Doe", "Address 1", "123456789", "john@example.com", "password", Roles.User, true);
            User user2 = new User(Guid.NewGuid(), "Jane", "Doe", "Address 2", "987654321", "jane@example.com", "password", Roles.Admin, true);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();

            // Act
            User result1 = repo.GetSingleOrDefaultByGuid<User>(user1.Guid);
            User result2 = repo.GetSingleOrDefaultByGuid<User>(user2.Guid);
            User result3 = repo.GetSingleOrDefaultByGuid<User>(Guid.NewGuid());

            // Assert
            Assert.Equal(user1, result1);
            Assert.Equal(user2, result2);
            Assert.Null(result3);
        }

       
    }
}