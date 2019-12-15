using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Dbo;
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
        private readonly IMapper _mapper;
        private readonly DbSet<GameDbo> _games;
        private readonly DbSet<PlayerDbo> _players;

        public GameRepository(HandicapContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _games = context.Set<GameDbo>();
            _players = context.Set<PlayerDbo>();
        }

        public async Task<Game> AddOrUpdate(Game game)
        {
            var gameDbo = _mapper.Map<GameDbo>(game);


            if (GameExists(gameDbo))
            {
                return await Update(gameDbo);
            }
            else
            {
                _context.Players.Attach(gameDbo.PlayerOne);
                _context.Players.Attach(gameDbo.PlayerTwo);
                return await Add(gameDbo);
            }
        }

        public async Task Delete(string id)
        {
            var gameDbo = _games.Where(g => g.Id == id).FirstOrDefault();

            if (gameDbo != null)
            {
                _games.Remove(gameDbo);
                await SaveChangesAsync();
            }
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

            var domainQuery = query.ProjectTo<Game>(_mapper.ConfigurationProvider);

            if (expression != null)
            {
                domainQuery = domainQuery.Where(expression);
            }

            return domainQuery;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<Game> Add(GameDbo gameDbo)
        {
            if (_games.Find(gameDbo.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Game with id {gameDbo.Id} already exists.");
            }

            _games.Add(gameDbo);
            await SaveChangesAsync();

            return _mapper.Map<Game>(gameDbo);
        }

        private async Task<Game> Update(GameDbo gameDbo)
        {
            if (gameDbo == null)
            {
                throw new EntityNotFoundException(
                    $"Game with id {gameDbo.Id} not found."
                    );
            }

            _context.Update(gameDbo);
            await SaveChangesAsync();

            return _mapper.Map<Game>(gameDbo);
        }

        private bool GameExists(GameDbo gameDbo)
        {
            var query = _games
                .AsNoTracking();

            if (query.Where(g => g.Id == gameDbo.Id).FirstOrDefault() != null) { return true; }

            return false;
        }
    }
}
