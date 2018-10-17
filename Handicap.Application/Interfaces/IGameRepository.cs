using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Interfaces
{
    public interface IGameRepository
    {
        Task Insert(Game game);
        Task<Game> GetById(Guid id);
        Task Update(Game game);
        Task Delete(Game game);
        Task<IQueryable<Game>> All(params string[] navigationProperties);
        Task SaveChangesAsync();
    }
}
