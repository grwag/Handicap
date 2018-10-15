using Handicap.Data.Repo;
using Handicap.Domain.Models;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Data.Exceptions;
using System;
using Handicap.Dto.Response.Paging;

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
            await _playerRepository.Insert(player);

            return await _playerRepository.GetById(player.Id);
        }

        public async Task<Player> GetById(Guid id)
        {
            var player = await _playerRepository.GetById(id);

            if (player == null)
            {
                throw new EntityNotFoundException($"Player with id {id} does not exist.");
            }

            return player;
        }

        public async Task Delete(Guid id)
        {
            var player = await _playerRepository.GetById(id);

            if (player == null)
            {
                throw new EntityNotFoundException($"Player with id {id} does not exist.");
            }

            _playerRepository.Delete(player);
            await _playerRepository.SaveChangesAsync();
        }

        public async Task<PagedList<Player>> All(PagingParameters pagingParameters)
        {
            var result = await _playerRepository.All(
                pagingParameters,
                false);

            return PagedList<Player>.Create(result, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
