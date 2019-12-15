using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Interfaces {
    public interface IPlayerRepository
    {
        Task<Player> AddOrUpdate(Player player);
        Task Delete(string id);
        Task<IQueryable<Player>> Find(Expression<Func<Player, bool>> expression = null);
        Task SaveChangesAsync();
    }
}