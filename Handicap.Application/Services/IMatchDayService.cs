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
        Task<MatchDay> AddPlayers(string matchDayId, IEnumerable<string> playerIds);
        Task<MatchDay> RemovePlayer(string matchDayId, string playerId);
        Task<MatchDay> GetById(string id);
        Task<MatchDay> AddGame(string matchDayId, string gameId);
        Task<MatchDay> AddGame(string matchDayId, Game game);
        Task<IQueryable<Player>> GetMatchDayPlayers(string matchDayId);
        Task<IQueryable<Game>> GetMatchDayGames(string matchDayId);
    }
}