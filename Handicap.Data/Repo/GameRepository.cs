using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<IQueryable<Game>> All(params string[] navigationProperties)
        {
            var query = _games.AsQueryable();

            foreach(var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return _mapper.Map<IEnumerable<Game>>(query).AsQueryable();
        }

        public async Task Delete(Game game)
        {
            var gameDbo = _mapper.Map<GameDbo>(game);
            _games.Remove(gameDbo);

            await SaveChangesAsync();
        }

        public async Task<Game> GetById(Guid id)
        {
            var query = _games.AsQueryable();
            query = query
                .Include($"{nameof(Game.PlayerOne)}")
                .Include($"{nameof(Game.PlayerTwo)}")
                ;

            var gameDbo = query.Where(g => g.Id == id)
                .SingleOrDefault();

            if(gameDbo == null)
            {
                throw new EntityNotFoundException($"Game with id {id} not found.");
            }

            return _mapper.Map<Game>(gameDbo);
        }

        public async Task<IQueryable<Game>> Find(
            Expression<Func<Game, bool>> expression,
            params string[] navigationProperties)
        {
            var query = _games.AsQueryable();

            foreach(var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            var domainQuery = query.ProjectTo<Game>(_mapper.ConfigurationProvider);

            if(expression != null)
            {
                domainQuery = domainQuery.Where(expression);
            }

            return domainQuery;
        }

        public async Task Insert(Game game)
        {
            if(_games.Find(game.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Game with id {game.Id} already exists.");
            }

            var gameDbo = _mapper.Map<GameDbo>(game);

            _games.Add(gameDbo);

            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(GameUpdate gameUpdate)
        {
            var gameDbo = _games.Where(
                g => g.Id == gameUpdate.Id)
                .SingleOrDefault();

            if(gameDbo == null)
            {
                throw new EntityNotFoundException(
                    $"Game with id {gameUpdate.Id} not found."
                    );
            }

            if (gameDbo.IsFinished)
            {
                throw new EntityClosedForUpdateException(
                    $"Game {gameDbo.Id} cannot be updated. It is already finished."
                    );
            }

            gameDbo.IsFinished = gameUpdate.IsFinished;
            gameDbo.PlayerOnePoints = gameUpdate.PlayerOnePoints;
            gameDbo.PlayerTwoPoints = gameUpdate.PlayerTwoPoints;

            _context.Entry(gameDbo).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}
