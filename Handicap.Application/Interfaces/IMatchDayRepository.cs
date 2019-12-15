using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Interfaces
{
    public interface IMatchDayRepository
    {
        Task<IQueryable<MatchDay>> All(params string[] navigationProperties);
        Task Insert(MatchDay matchDay);
        Task Update(MatchDay matchDay);
        Task<MatchDay> GetById(string id);
        void Delete(MatchDay matchDay);
        Task SaveChangesAsync();
    }
}
