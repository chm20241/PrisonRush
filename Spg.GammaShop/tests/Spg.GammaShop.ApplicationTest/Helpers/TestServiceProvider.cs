using Spg.GammaShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Domain;
using Spg.GammaShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.GammaShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spg.GammaShop.Domain.Interfaces;
using Spg.GammaShop.Application.Services.CQS;

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public class TestServiceProvider //: IServiceProvider
    {

        private GammaShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  .UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  //.UseSqlite(ReadLineWithQuestionMark())     //Home PC       
                .Options;

            GammaShopContext db = new GammaShopContext(options);
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }

        private GammaShopContext createDB_Del_Create_DB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(ReadLineWithQuestionMark())     //Home PC       
                .Options;

            GammaShopContext db = new GammaShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
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
    }

}
