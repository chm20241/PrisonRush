﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces.RepoBase_Interfaces
{
    public interface IRepositoryBase_Old<TEntity>
    {
        TEntity? Create(TEntity entity);
        TEntity? Update(TEntity entity);
        TEntity? Delete(TEntity entity);
    }
}
