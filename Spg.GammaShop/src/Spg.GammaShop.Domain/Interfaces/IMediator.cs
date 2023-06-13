using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Interfaces
{
    public interface IMediator
    {
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query);
    }

}
