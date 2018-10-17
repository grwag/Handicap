using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handicap.Application.Interfaces;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IQueryable<Game>> All()
        {
            var games = await _gameRepository.All(
                $"{nameof(Game.PlayerOne)}",
                $"{nameof(Game.PlayerTwo)}");

            return games;
        }

        public async Task Delete(Guid Id)
        {
            var game = await _gameRepository.GetById(Id);
            await _gameRepository.Delete(game);
        }

        public async Task<Game> GetById(Guid Id)
        {
            var game = await _gameRepository.GetById(Id);

            return game;
        }

        public async Task<Game> Insert(Game game)
        {
            await _gameRepository.Insert(game);

            return await _gameRepository.GetById(game.Id);
        }

        public Task<Game> Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
