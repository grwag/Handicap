using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IMatchDayService
    {
        Task<IQueryable<MatchDay>> Find(
            Expression<Func<MatchDay, bool>> expression,
            params string[] navigationProperties);
        Task<MatchDay> CreateMatchDay(string tenantId);
        Task<MatchDay> AddPlayers(MatchDay matchDay, IEnumerable<string> playerIds);
        Task<MatchDay> GetById(string id);
        Task<MatchDay> AddGame(string matchDayId, string gameId);
    }
}