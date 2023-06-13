﻿using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Domain.Interfaces.RepoBase_Interfaces;
using Spg.GammaShop.Domain.Exeptions;

namespace Spg.GammaShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase_Old<TEntity> where TEntity : class
    {
        protected readonly GammaShopContext _db;

        public RepositoryBase(GammaShopContext db)
        {
            _db = db;
        }

        public TEntity? Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new RepositoryCreateException($"{nameof(TEntity)} war NULL!");
            }
            
            _db.Set<TEntity>().Add(entity);
            
            try
            {
                _db.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                throw new RepositoryCreateException($"{nameof(TEntity)} konnte nicht gespeichert werden!", e);
            }
        }

        public TEntity? Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity? Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}