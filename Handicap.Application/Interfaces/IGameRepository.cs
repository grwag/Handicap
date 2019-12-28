using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> AddOrUpdate(Game game);
        Task<IQueryable<Game>> Find(Expression<Func<Game, bool>> expression = null,
            params string[] navigationProperties);
        Task Delete(string id, string tenantId);
        Task SaveChangesAsync();
    }
}
