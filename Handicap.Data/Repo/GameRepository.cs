using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class GameRepository : IGameRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<Game> _games;
        private readonly DbSet<Player> _players;

        public GameRepository(HandicapContext context)
        {
            _context = context;
            _games = context.Set<Game>();
            _players = context.Set<Player>();
        }

        public async Task<Game> AddOrUpdate(Game game)
        {
            //_context.MatchDays.Attach(gameDbo.MatchDay);

            if (GameExists(game))
            {
                return await Update(game);
            }
            else
            {
                _context.Players.Attach(game.PlayerOne);
                _context.Players.Attach(game.PlayerTwo);
                return await Add(game);
            }
        }

        public async Task Delete(string id, string tenantId)
        {
            var game = _games.Where(g => g.Id == id).FirstOrDefault();

            if(game == null)
            {
                throw new EntityNotFoundException($"Game with id {id} not found.");
            }

            if(game.TenantId != tenantId)
            {
                throw new TenantMissmatchException();
            }

            _games.Remove(game);
            await SaveChangesAsync();
        }

        public async Task<IQueryable<Game>> Find(
            Expression<Func<Game, bool>> expression,
            params string[] navigationProperties)
        {
            var query = _games
                .AsQueryable()
                .AsNoTracking();

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<Game> Add(Game game)
        {
            if (_games.Find(game.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Game with id {game.Id} already exists.");
            }

            _games.Add(game);

            return game;
        }

        private async Task<Game> Update(Game game)
        {
            if (game == null)
            {
                throw new EntityNotFoundException(
                    $"Game with id {game.Id} not found."
                    );
            }

            _context.Update(game);

            return game;
        }

        private bool GameExists(Game game)
        {
            var query = _games
                .AsNoTracking();

            if (query.Where(g => g.Id == game.Id).FirstOrDefault() != null) { return true; }

            return false;
        }
    }
}
