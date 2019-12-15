using Handicap.Domain.Models;
using System.Threading.Tasks;
using System;
using Handicap.Application.Interfaces;
using System.Linq;

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
            return await _playerRepository.AddOrUpdate(player);
        }

        public async Task<Player> GetById(string id)
        {
            var player = (await _playerRepository.Find(p => p.Id == id)).FirstOrDefault();

            return player;
        }

        public async Task Delete(string id)
        {
            var player = (await _playerRepository.Find(p => p.Id == id)).FirstOrDefault();

            await _playerRepository.Delete(id);
        }

        public async Task<IQueryable<Player>> All()
        {
            var result = await _playerRepository.Find();

            return result;
        }

        public async Task<Player> Update(Player player)
        {
            var updatedPlayer = await _playerRepository.AddOrUpdate(player);

            return updatedPlayer;
        }
    }
}
