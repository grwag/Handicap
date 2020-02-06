using Handicap.Domain.Models;
using System.Threading.Tasks;
using System;
using Handicap.Application.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using Handicap.Application.Exceptions;

namespace Handicap.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> AddOrUpdate(Player player)
        {
            await _playerRepository.AddOrUpdate(player);
            await _playerRepository.SaveChangesAsync();

            return player;
        }

        public async Task<IQueryable<Player>> Find(Expression<Func<Player, bool>> expression = null)
        {
            var query = await _playerRepository.Find(expression);

            return query;
        }

        public async Task<Player> GetById(string id, string tenantId)
        {
            var player = (await _playerRepository.Find(p => p.Id == id && p.TenantId == tenantId))
                .FirstOrDefault();

            if(player == null)
            {
                throw new EntityNotFoundException($"Player with id {id} not found.");
            }

            return player;
        }

        public async Task Delete(string id, string tenantId)
        {
            var player = (await _playerRepository.Find(
                p => p.Id == id && p.TenantId == tenantId,
                nameof(Player.MatchDayPlayers),
                $"{nameof(Player.MatchDayPlayers)}.{nameof(MatchDayPlayer.MatchDay)}"))
                .FirstOrDefault();

            if (player == null)
            {
                throw new EntityNotFoundException($"Player with id {id} not found.");
            }

            if(player.MatchDayPlayers.Any(mdp => mdp.MatchDay.IsFinished == false)) {
                throw new EntityClosedForUpdateException($"Cannot delete player {player.Id}. He is part of an open Matchday");
            }

            await _playerRepository.Delete(id);
            await _playerRepository.SaveChangesAsync();
        }
    }
}
