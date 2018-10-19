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
        private readonly IPlayerRepository _playerRepository;
        private readonly IHandicapCalculator _handicapCalculator;

        public GameService(IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IHandicapCalculator handicapCalculator)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _handicapCalculator = handicapCalculator;
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

        public async Task<Game> Insert(Guid PlayerOneId, Guid PlayerTwoId)
        {
            var playerOne = await _playerRepository.GetById(PlayerOneId);
            var playerTwo = await _playerRepository.GetById(PlayerTwoId);

            var game = new Game();
            game.SetGameType();

            game.PlayerOne = playerOne;
            game.PlayerTwo = playerTwo;

            game.PlayerOneRequiredPoints = _handicapCalculator.Calculate(
                game.PlayerOne.Handicap, game.Type);

            game.PlayerTwoRequiredPoints = _handicapCalculator.Calculate(
                game.PlayerTwo.Handicap, game.Type);

            await _gameRepository.Insert(game);

            return await _gameRepository.GetById(game.Id);
        }

        public Task<Game> Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
