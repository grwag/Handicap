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
        private readonly IHandicapConfigurationService _configService;
        private readonly IHandicapUpdateService _handicapUpdateService;
        private readonly IPlayerRepository _playerRepository;

        public MatchDayService(IMatchDayRepository matchDayRepository,
            IGameRepository gameRepository,
            IHandicapConfigurationService configService,
            IHandicapUpdateService handicapUpdateService,
            IPlayerRepository playerRepository)
        {
            _matchDayRepository = matchDayRepository;
            _gameRepository = gameRepository;
            _configService = configService;
            _handicapUpdateService = handicapUpdateService;
            _playerRepository = playerRepository;
        }

        public async Task<MatchDay> AddPlayers(string matchDayId, IEnumerable<string> playerIds, string tenantId)
        {
            var matchDay = await GetById(matchDayId);

            if (matchDay.IsFinished)
            {
                throw new EntityClosedForUpdateException($"MatchDay with id {matchDayId} is already finished.");
            }

            foreach (var playerId in playerIds)
            {
                var player = (await _playerRepository.Find(p => p.Id == playerId)).FirstOrDefault();
                if (player != null)
                {
                    if(player.TenantId != tenantId)
                    {
                        throw new TenantMissmatchException();
                    }

                    if (!MatchDayHasPlayer(matchDay, player))
                    {
                        matchDay.MatchDayPlayers.Add(new MatchDayPlayer
                        {
                            Player = player
                        });
                    }
                }
            }

            await _matchDayRepository.UpdateMatchDayPlayers(matchDay);
            await _matchDayRepository.SaveChangesAsync();

            return matchDay;
        }

        public async Task<MatchDay> RemovePlayer(string matchDayId, string playerId, string tenantId)
        {
            var matchDay = await GetById(matchDayId);

            if(matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            if (matchDay.IsFinished)
            {
                throw new EntityClosedForUpdateException($"MatchDay with id {matchDayId} is already finished.");
            }

            var player = (await _playerRepository.Find(p => p.Id == playerId)).FirstOrDefault();
            if (player != null)
            {
                if(player.TenantId != tenantId)
                {
                    throw new TenantMissmatchException();
                }

                if (MatchDayHasPlayer(matchDay, player))
                {
                    var mdp = matchDay.MatchDayPlayers.Where(md => md.PlayerId == player.Id).FirstOrDefault();

                    if (mdp != null)
                    {
                        matchDay.MatchDayPlayers.Remove(mdp);

                        await _matchDayRepository.UpdateMatchDayPlayers(matchDay);
                        await _matchDayRepository.SaveChangesAsync();
                    }
                }
            }

            return matchDay;
        }

        public async Task<MatchDay> CreateMatchDay(string tenantId)
        {
            var matchDay = new MatchDay();
            matchDay.TenantId = tenantId;

            await _matchDayRepository.Insert(matchDay);
            await _matchDayRepository.SaveChangesAsync();
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

        public async Task<MatchDay> AddGame(string matchDayId, string gameId, string tenantId)
        {
            var matchDay = (await _matchDayRepository
                .Find(md => md.Id == matchDayId,
                nameof(MatchDay.Games)))
                .FirstOrDefault();

            if (matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay with id: {matchDayId} not found.");
            }

            if(matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            if (matchDay.IsFinished)
            {
                throw new EntityClosedForUpdateException($"MatchDay with id {matchDayId} is already finished.");
            }

            var game = (await _gameRepository.Find(
                g => g.Id == gameId))
                .FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFoundException($"Game with id: {gameId} not found.");
            }

            if(game.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            if (game.IsFinished)
            {
                throw new EntityClosedForUpdateException($"Game with id {gameId} is already finished.");
            }


            matchDay.Games.Add(game);

            matchDay = await _matchDayRepository.Update(matchDay);
            await _matchDayRepository.SaveChangesAsync();

            return matchDay;
        }

        public async Task<IQueryable<Player>> GetMatchDayPlayers(string matchDayId, string tenantId)
        {
            var matchDay = (await _matchDayRepository
                .Find(md => md.Id == matchDayId,
                $"{nameof(MatchDay.MatchDayPlayers)}",
                $"{nameof(MatchDay.MatchDayPlayers)}.{nameof(MatchDayPlayer.Player)}"))
                .FirstOrDefault();

            if(matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            var playersQuery = matchDay.MatchDayPlayers.AsQueryable();
            var players = playersQuery.Select(p => p.Player);

            return players;
        }

        public async Task<IQueryable<Game>> GetMatchDayGames(string matchDayId, string tenantId)
        {
            var matchDay = (await _matchDayRepository.Find(md => md.Id == matchDayId,
                nameof(MatchDay.Games),
                $"{nameof(MatchDay.Games)}.{nameof(Game.PlayerOne)}",
                $"{nameof(MatchDay.Games)}.{nameof(Game.PlayerTwo)}"))
                .FirstOrDefault();

            if(matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay wit id {matchDayId} not found.");
            }

            if(matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            return matchDay.Games.AsQueryable();
        }

        public async Task<MatchDay> FinalizeMatchDay(string matchDayId, string tenantId)
        {
            var matchDay = (await _matchDayRepository
                .Find(md => md.Id == matchDayId,
                nameof(MatchDay.Games)))
                //$"{nameof(MatchDay.Games)}.{nameof(Game.PlayerOne)}",
                //$"{nameof(MatchDay.Games)}.{nameof(Game.PlayerTwo)}"))
                .FirstOrDefault();

            if (matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay with id {matchDayId} not found.");
            }

            if (matchDay.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            if (matchDay.IsFinished)
            {
                throw new EntityClosedForUpdateException($"MatchDay with id {matchDayId} is already finished.");
            }

            var config = await _configService.Get(tenantId);

            if (!config.UpdatePlayersImmediately)
            {
                var playerResults = GetPlayersResults(matchDay);

                foreach (var playerResult in playerResults)
                {
                    var player = (await _playerRepository.Find(p => p.Id == playerResult.PlayerId)).FirstOrDefault();
                    if (player != null)
                    {
                        player.Handicap += playerResult.HandicapToAdd;
                        await _playerRepository.AddOrUpdate(player);
                    }
                }
            }

            matchDay.IsFinished = true;

            await _matchDayRepository.Update(matchDay);
            await _matchDayRepository.SaveChangesAsync();

            return matchDay;
        }

        private bool MatchDayHasPlayer(MatchDay matchDay, Player player)
        {
            var tstPlayer = matchDay.MatchDayPlayers
            .Where(p => p.PlayerId == player.Id)
            .FirstOrDefault();

            return tstPlayer != null;
        }

        private List<(string PlayerId, int HandicapToAdd)> GetPlayersResults(MatchDay matchDay)
        {
            var results = new List<(string PlayerId, int HandicapToAdd)>();

            var playerIds = matchDay.Games.Select(g => g.PlayerOneId).ToList();
            playerIds.AddRange(matchDay.Games.Select(g => g.PlayerTwoId).ToList());

            playerIds = playerIds.Distinct().ToList();



            foreach (var playerId in playerIds)
            {
                var handicapToAdd = 0;
                foreach (var game in matchDay.Games)
                {
                    handicapToAdd += HandicapToAdd(playerId, game);
                }

                results.Add((PlayerId: playerId, HandicapToAdd: handicapToAdd));
            }

            return results;
        }

        private int HandicapToAdd(string playerId, Game game)
        {
            if (game.PlayerOneId != playerId && game.PlayerTwoId != playerId) { return 0; }

            if (game.PlayerOnePoints >= game.PlayerOneRequiredPoints)
            {
                if (playerId == game.PlayerOneId)
                {
                    return -5;
                }

                return 5;
            }
            else
            {
                if (playerId == game.PlayerTwoId)
                {
                    return -5;
                }

                return 5;
            }
        }
    }
}
