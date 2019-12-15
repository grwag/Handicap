using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IPlayerService
    {
        Task<Player> InsertPlayer(Player player);
        Task<Player> GetById(string id);
        Task Delete(string id);
        Task<Player> Update(Player player);
        Task<IQueryable<Player>> All();
    }
}
