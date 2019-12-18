using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public class MatchDayService : IMatchDayService
    {
        private readonly IMatchDayRepository _matchDayRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;

        public MatchDayService(IMatchDayRepository matchDayRepository,
            IGameRepository gameRepository,
            IPlayerRepository playerRepository)
        {
            _matchDayRepository = matchDayRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<MatchDay> AddPlayers(MatchDay matchDay, IEnumerable<string> playerIds)
        {
            foreach(var playerId in playerIds)
            {
                var player = (await _playerRepository.Find(p => p.Id == playerId)).FirstOrDefault();
                if(player != null)
                {
                    //matchDay.Players.Add(player);
                }
            }

            await _matchDayRepository.Update(matchDay);
            await _matchDayRepository.SaveChangesAsync();

            return matchDay;
        }

        public async Task<MatchDay> CreateMatchDay(string tenantId)
        {
            var matchDay = new MatchDay();
            matchDay.TenantId = tenantId;

            await _matchDayRepository.Insert(matchDay);

            return matchDay;
        }

        public async Task<IQueryable<MatchDay>> Find(Expression<Func<MatchDay, bool>> expression, params string[] navigationProperties)
        {
            return await _matchDayRepository.Find(expression, navigationProperties);
        }

        public async Task<MatchDay> GetById(string id)
        {
            var matchDay = await _matchDayRepository.GetById(id);

            return matchDay;
        }

        public async Task<MatchDay> AddGame(string matchDayId, string gameId)
        {
            var matchDay = (await _matchDayRepository
                .Find(md => md.Id == matchDayId, nameof(MatchDay.Games)))
                .FirstOrDefault();

            var game = (await _gameRepository.Find(g => g.Id == gameId)).FirstOrDefault();
            if(matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay with id: {matchDayId} not found.");
            }

            matchDay.Games.Add(game);

            matchDay = await _matchDayRepository.Update(matchDay);

            return matchDay;
        }
    }
}
