using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Interfaces
{
    public interface IMatchDayRepository
    {
        Task<IQueryable<MatchDay>> Find(
            Expression<Func<MatchDay, bool>> expression,
            params string[] navigationProperties);
        Task Insert(MatchDay matchDay);
        Task<MatchDay> Update(MatchDay matchDay);
        Task<MatchDay> UpdateMatchDayPlayers(MatchDay matchDay);
        Task<MatchDay> GetById(string id);
        void Delete(MatchDay matchDay);
        Task SaveChangesAsync();
    }
}
