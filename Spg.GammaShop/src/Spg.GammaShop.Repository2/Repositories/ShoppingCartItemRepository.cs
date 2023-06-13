﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using Spg.GammaShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Repository2.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly GammaShopContext _db;

        public ShoppingCartItemRepository(GammaShopContext db)
        {
            _db = db;
        }

        public ShoppingCartItem Add(ShoppingCartItem shoppingCartItem)
        {
           _db.ShoppingCartItems.Add(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Remove(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public List<ShoppingCartItem> GetAll()
        {
            return _db.ShoppingCartItems.Include(s => s.ShoppingCartNav.UserNav).ToList();
        }

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return (ShoppingCartItem)(_db.ShoppingCartItems.Include(s => s.ShoppingCartNav.UserNav).Where(s => s.guid == guid) ?? throw new KeyNotFoundException($"No Item found with Guid {guid}"));
        }

        public ShoppingCartItem GetById(int Id)
        {
            return (ShoppingCartItem)(_db.ShoppingCartItems.Include(s => s.ShoppingCartNav.UserNav).Where(s => s.Id == Id) ?? throw new KeyNotFoundException($"No Item found with Guid {Id}"));
        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Update(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public List<ShoppingCartItem> GetAllIncludeShoppingCartNav()
        {
            return _db.ShoppingCartItems.Include(s => s.ShoppingCartNav).ToList();
        }
    }
}
