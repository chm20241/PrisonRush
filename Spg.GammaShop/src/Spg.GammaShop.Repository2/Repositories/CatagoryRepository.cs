using Spg.GammaShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Repository2.Repositories
{
    public class CatagoryRepository : ICatagoryRepository
    {
        private readonly GammaShopContext _db;

        public CatagoryRepository(GammaShopContext db)
        {
            _db = db;
        }

        public Catagory AddCatagory(Catagory catagory)
        {
            _db.Catagories.Add(catagory);
            _db.SaveChanges();
            return catagory;
        }

        public void DeleteCatagory(Catagory catagory)
        {
            _db.Catagories.Remove(catagory);
            _db.SaveChanges();
        }

        public IEnumerable<Catagory> GetAllCatagories()
        {
            return _db.Catagories.ToList();
        }

        public IEnumerable<Catagory> GetCatagoriesByTopCatagory(Catagory topCatagory)
        {
            return _db.Catagories.Where(c => c.TopCatagory == topCatagory).ToList();
        }

        public IEnumerable<Catagory> GetCatagoriesByType(CategoryTypes categoryType)
        {
            return _db.Catagories.Where(c => c.CategoryType == categoryType).ToList();
        }

        public Catagory GetCatagoryById(int id)
        {
            return _db.Catagories.Find(id) ?? throw  new Exception($"Catagory with Id: {id} not found");
    }

        public Catagory GetCatagoryByName(string name)
        {
            return _db.Catagories.Where(c => c.Name == name).SingleOrDefault() ?? throw new Exception($"Catagory with Name: {name} not found");

        }

        public string GetCatagoryDescriptionById(int id)
        {
            return _db.Catagories.Find(id)?.Description ?? throw new Exception($"Catagory with Id: {id} not found");
        }

        public Catagory UpdateCatagory(Catagory catagory)
        {
            _db.Catagories.Update(catagory);
            _db.SaveChanges();
            return catagory;
        }
    }
}
