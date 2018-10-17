using AutoMapper;
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
            _games = _context.Set<GameDbo>();
            _players = _context.Set<PlayerDbo>();
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
            var gameDbo = _games.Where(g => g.Id == id)
                .SingleOrDefault();

            if(gameDbo == null)
            {
                throw new EntityNotFoundException($"Game with id {id} not found.");
            }

            return _mapper.Map<Game>(gameDbo);
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

        public Task Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
