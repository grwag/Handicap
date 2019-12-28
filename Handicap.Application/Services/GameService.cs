using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Handicap.Application.Exceptions;
using Handicap.Application.Extensions;
using Handicap.Application.Interfaces;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchDayService _matchDayService;
        private readonly IHandicapUpdateService _handicapUpdateService;
        private readonly IHandicapConfigurationService _configService;
        private readonly IHandicapCalculator _handicapCalculator;

        public GameService(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IMatchDayService matchDayService,
            IHandicapUpdateService handicapUpdateService,
            IHandicapConfigurationService configService,
            IHandicapCalculator handicapCalculator)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _matchDayService = matchDayService;
            _handicapUpdateService = handicapUpdateService;
            _configService = configService;
            _handicapCalculator = handicapCalculator;
        }

        public async Task<IQueryable<Game>> Find(
            Expression<Func<Game, bool>> expression,
            params string[] navigationProperties)
        {
            return await _gameRepository.Find(expression, navigationProperties);
        }

        public async Task Delete(string id, string tenantId)
        {
            await _gameRepository.Delete(id, tenantId);
        }

        public async Task<Game> GetById(string Id)
        {
            var game = await _gameRepository.Find(
                g => g.Id == Id,
                $"{nameof(Game.PlayerOne)}",
                $"{nameof(Game.PlayerTwo)}");

            return game.FirstOrDefault();
        }

        public async Task<IQueryable<Game>> GetGamesForPlayer(string playerId)
        {
            var player = (await _playerRepository.Find(p => p.Id == playerId)).FirstOrDefault();

            var games = await _gameRepository.Find(
                g => g.PlayerOne.Id == player.Id || g.PlayerTwo.Id == player.Id,
                $"{nameof(Game.PlayerOne)}",
                $"{nameof(Game.PlayerTwo)}");

            return games;
        }

        public async Task<Game> CreateGame(string TenantId, string PlayerOneId, string PlayerTwoId, string MatchDayId)
        {
            var playerOne = (await _playerRepository.Find(p => p.Id == PlayerOneId)).FirstOrDefault();
            var playerTwo = (await _playerRepository.Find(p => p.Id == PlayerTwoId)).FirstOrDefault();

            var game = new Game();
            game.TenantId = TenantId;
            game.SetGameType();

            game.PlayerOne = playerOne;
            game.PlayerTwo = playerTwo;

            game.PlayerOneRequiredPoints = _handicapCalculator.Calculate(
                game.PlayerOne.Handicap, game.Type);

            game.PlayerTwoRequiredPoints = _handicapCalculator.Calculate(
                game.PlayerTwo.Handicap, game.Type);

            return game;
        }

        public async Task<Game> Add(Game game)
        {
            await _gameRepository.AddOrUpdate(game);
            await _gameRepository.SaveChangesAsync();

            return game;
        }

        public async Task<Game> Update(GameUpdate gameUpdate)
        {
            var query = await _gameRepository.Find(
                g => g.Id == gameUpdate.Id,
                $"{nameof(Game.PlayerOne)}",
                $"{nameof(Game.PlayerTwo)}");

            var game = query.FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFoundException($"Game with id: {gameUpdate.Id} not found.");
            }

            if (game.IsFinished)
            {
                throw new EntityClosedForUpdateException(
                    $"Game {game.Id} cannot be updated. It is already finished."
                    );
            }

            var config = await _configService.Get(game.TenantId);

            game.PlayerOnePoints = gameUpdate.PlayerOnePoints;
            game.PlayerTwoPoints = gameUpdate.PlayerTwoPoints;
            game.IsFinished = IsFinished(game);

            if (game.IsFinished && config.UpdatePlayersImmediately)
            {
                game = _handicapUpdateService.UpdatePlayerHandicap(game); 
            }

            game = await _gameRepository.AddOrUpdate(game);
            await _gameRepository.SaveChangesAsync();

            return game;
        }

        public async Task<Game> CreateNewGameForMatchDay(string matchDayId, string tenantId)
        {
            var matchDay = (await _matchDayService.Find(m => m.Id == matchDayId, nameof(MatchDay.Games))).FirstOrDefault();

            if(matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay with id {matchDay} not found.");
            }

            if(matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            var matchDayGames = (await _matchDayService.GetMatchDayGames(matchDayId, tenantId)).ToList();
            var nextPlayers = matchDay.GetNextPlayers(matchDayGames);

            var game = await CreateGame(matchDay.TenantId, nextPlayers.PlayerOneId, nextPlayers.PlayerTwoId, matchDay.Id);
            game = await Add(game);

            return game;
        }

        private bool IsFinished(Game game)
        {
            return ((game.PlayerOnePoints >= game.PlayerOneRequiredPoints)
                || (game.PlayerTwoPoints >= game.PlayerTwoRequiredPoints));
        }
    }
}
