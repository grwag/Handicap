using AutoMapper;
using Handicap.Data.Exceptions;
using Handicap.Data.Infrastructure;
using Handicap.Data.Paging;
using Handicap.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<PlayerDbo> _entities;
        private readonly IMapper _mapper;

        public PlayerRepository(HandicapContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _entities = context.Set<PlayerDbo>();
        }

        public async Task Insert(PlayerDbo playerDbo)
        {
            if (_entities.Find(playerDbo.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Player '{playerDbo.FirstName} {playerDbo.LastName}' already exists.");
            }

            var checkPlayer = _entities.Where(
                p => p.FirstName == playerDbo.FirstName
                && p.LastName == playerDbo.LastName);

            if (checkPlayer.Any())
            {
                throw new EntityAlreadyExistsException($"Player {playerDbo.FirstName} {playerDbo.LastName} already exists.");
            }

            _entities.Add(playerDbo);
            await SaveChangesAsync();
        }

        public async Task<IQueryable<PlayerDbo>> All(
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties)
        {
            var query = _entities.AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            return query;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PlayerDbo> GetById(Guid id)
        {
            var playerDbo = _entities.Where(
                p => p.Id == id);

            return playerDbo.FirstOrDefault();
        }

        public void Delete(PlayerDbo playerDbo)
        {
            _entities.Remove(playerDbo);
        }
    }
}
