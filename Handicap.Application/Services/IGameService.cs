using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public interface IGameService
    {
        Task<Game> Insert(Guid PlayerOneId, Guid PlayerTwoId);
        Task Delete(Guid Id);
        Task Update(GameUpdate gameUpdate);
        Task<IQueryable<Game>> All();
        Task<Game> GetById(Guid Id);
    }
}
