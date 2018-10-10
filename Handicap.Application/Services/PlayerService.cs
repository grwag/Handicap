using Handicap.Data.Repo;
using Handicap.Domain.Models;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Data.Exceptions;
using System.Linq;
using System;
using Handicap.Dto.Response.Paging;
using System.Linq.Expressions;

namespace Handicap.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> InsertPlayer(Player player)
        {
            var checkPlayer = await _playerRepository.FindAsync(
                p => p.FirstName == player.FirstName && p.LastName == player.LastName,
                p => p.LastName,
                new PagingParameters());

            if(checkPlayer.Any())
            {
                throw new EntityAlreadyExistsException($"Player {player.FirstName} {player.LastName} already exists.");
            }

            player = await _playerRepository.Insert(player);

            return player;
        }

        public async Task<PagedList<Player>> FindAsync(Expression<Func<Player, bool>> expression, PagingParameters pagingParameters)
        {
            var result = await _playerRepository.FindAsync(
                expression,
                o => o.LastName,
                pagingParameters,
                false);

            return result;
        }
    }
}
