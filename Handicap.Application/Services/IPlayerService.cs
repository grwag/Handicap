using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IPlayerService
    {
        Task<Player> AddOrUpdate(Player player);
        Task<IQueryable<Player>> Find(Expression<Func<Player, bool>> expression = null);
        Task<Player> GetById(string id, string tenantId);
        Task Delete(string id, string tenantId);
    }
}
