using System;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Interfaces {
    public interface IPlayerRepository
    {
        Task<IQueryable<Player>> All(params string[] navigationProperties);

        Task Insert(Player player);
        void Delete(Player player);
        Task<Player> GetById(Guid id);
        Task SaveChangesAsync();
    }
}