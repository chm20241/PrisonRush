using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using Spg.GammaShop.API.Controllers.V1;
using Spg.GammaShop.Application.Services;
using Spg.GammaShop.Domain.DTO;
using Spg.GammaShop.Domain.Interfaces.UserInterfaces;
using Spg.GammaShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Test
{
    [Collection("Sequential tests")]
    public class UserRegisterController_UnitTest
    {
        
        private UserRepository _userRepo;
        private UserMailRepo _userMailRepository;
        private UserRegistServic _userRegistServic;
        private RegisterController _registerController;
        private UserMailService _userMailService;

        private RegisterController getController(GammaShopContext db)
        {
            _userRepo = new(db);
            _userMailRepository = new(db);
            _userMailService = new(_userMailRepository);
            _userRegistServic = new(_userRepo, _userMailRepository, _userMailService);
            return _registerController = new(_userRegistServic);
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

        private GammaShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  .UseSqlite(ReadLineWithQuestionMark())
                  .UseSqlite(@"Data Source = C:\Users\bernd\Documents\Spg.GammaShop")     //Home PC       
                .Options;

            GammaShopContext db = new GammaShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }

        [Fact]
        public void Controller_Register_Test()
        {
            UserRegistDTO userDTOInput = new() { Addrese = "TestAddrese", Email = "berndMailEmpfangTestSPG@web.de", Nachname = "TestNachname", PW = "testPW", Telefon = "133", Vorname = "testVorname" };

            var db = createDB();

            RegisterController controller = getController(db);

        }

    }
}
