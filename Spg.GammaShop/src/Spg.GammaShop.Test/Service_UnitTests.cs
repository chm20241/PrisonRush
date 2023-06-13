﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spg.GammaShop.Application;
using Spg.GammaShop.Application.Services;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Repository2.Repositories;

namespace Spg.GammaShop.Domain.Test
{
    [Collection("Sequential tests")]
    public class Service_UnitTests
    {
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

        private UserMailService Get_Service_UserMail(GammaShopContext _db) 
        {
            UserMailRepo userMailRepo = new UserMailRepo(_db);
            return new UserMailService(userMailRepo);
        }

        private UserRegistServic Get_Service_UserRegist(GammaShopContext _db) 
        {
            UserRepository userRepo = new(_db);
            UserMailRepo userMailRepo = new(_db);
            UserMailService _userMailService = new(userMailRepo); 

            return new UserRegistServic(userRepo, userMailRepo, _userMailService);
            
        }

        [Fact]
        public void Service_SendMail_Test()
        {
            GammaShopContext db = createDB();
            UserRegistServic userRegist = Get_Service_UserRegist(db);
            User userPost = new User(Guid.NewGuid(), "TestVorname", "TestNachname", "TestAdresse", "06762656646", "davidMailEmpfangTestSPG@web.de", "TestPasswort", Roles.User, false);

            User user = (User)userRegist.Register_sendMail_Create_User(userPost, "mailtestdavid01@gmail.com").First();

        }

        [Fact]
        public void Service_SendMail_and_Check_Code_Test()
        {
            GammaShopContext db = createDB();
            UserRegistServic userRegist = Get_Service_UserRegist(db);
            // Guid guid, string vorname, string nachname,string addrese, string telefon, string email, string pW, Roles role, bool confirmed
            User userPost = new User(Guid.NewGuid(), "TestVorname", "TestNachname", "TestAdresse", "06762656646", "davidMailEmpfangTestSPG@web.de", "TestPasswort", Roles.User, false) ;
            var obj = userRegist.Register_sendMail_Create_User(userPost, "mailtestdavid01@gmail.com");

            UserMailService userMailService = Get_Service_UserMail(db);

            User user = (User)obj.First();
            UserMailConfirme userMailConfirme = userMailService.GetUserMailConfirmeByMail(user.Email);

            Assert.Equal(userMailConfirme.Code, ComputeSha256Hash(obj.Last().ToString()));
        }

        [Fact]
        public void Service_SendMail_and_Check_Code_and_Check_User_Status_Test()
        {
            GammaShopContext db = createDB();
            UserRegistServic userRegistService = Get_Service_UserRegist(db);

            User userPost = new User(Guid.NewGuid(), "TestVorname", "TestNachname", "TestAdresse", "06762656646", "davidMailEmpfangTestSPG@web.de", "TestPasswort", Roles.User, false);

            var UserCodeStore = userRegistService.Register_sendMail_Create_User(userPost, "mailtestdavid01@gmail.com");

            UserMailService userMailService = Get_Service_UserMail(db);

            User user = (User)UserCodeStore.First();
            UserMailConfirme userMailConfirme = userMailService.GetUserMailConfirmeByMail(user.Email);


            Assert.True(!user.Confirmed);
            Assert.True(userRegistService.CheckCode_and_verify(user.Email, (string)UserCodeStore.Last()));
            Assert.True(user.Confirmed);

            Assert.Equal(userMailConfirme.Code, ComputeSha256Hash(UserCodeStore.Last().ToString()));
        }

        [Fact]
        public void Repo_CreateUser_Test() 
        {
            GammaShopContext db = createDB();
            UserRepository userRepository = new(db);
            User user = new User(Guid.NewGuid(), "TestVorname", "TestNachname", "TestAdresse", "06762656646", "davidMailEmpfangTestSPG@web.de", "TestPasswort", Roles.User, false);
            User ur = userRepository.SetUser(user);

            Assert.Equal(user, ur);

            Assert.True(db.Users.Count() == 1); ;
        }

        [Fact]
        public void Service_Mail_Check_Test() 
        {
            SendMail sm = new();
            Assert.True(sm.ValidateMail("bernd.chm20241@gmail.com"));
           // Assert.True(sm.ValidateMail("david.ankenbrand@gmx.at"));
            Assert.False(sm.ValidateMail("test425236551safasfasf@gmail.com"));
            Assert.False(sm.ValidateMail("bernd.chm20241@testXY.com"));
        }


        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
