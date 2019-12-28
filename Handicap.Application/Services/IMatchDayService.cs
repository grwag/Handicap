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
        Task<MatchDay> AddPlayers(string matchDayId, IEnumerable<string> playerIds, string tenantId);
        Task<MatchDay> RemovePlayer(string matchDayId, string playerId, string tenantId);
        Task<MatchDay> GetById(string id);
        Task<MatchDay> AddGame(string matchDayId, string gameId, string tenantId);
        Task<IQueryable<Player>> GetMatchDayPlayers(string matchDayId, string tenantId);
        Task<IQueryable<Game>> GetMatchDayGames(string matchDayId, string tenantId);
        Task<MatchDay> FinalizeMatchDay(string matchDayId, string tenantId);
    }
}