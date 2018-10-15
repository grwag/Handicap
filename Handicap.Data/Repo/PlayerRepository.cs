using AutoMapper;
using Handicap.Data.Exceptions;
using Handicap.Data.Infrastructure;
using Handicap.Data.Paging;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task Insert(Player player)
        {
            if (_entities.Find(player.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Player '{player.FirstName} {player.LastName}' already exists.");
            }

            var checkPlayer = _entities.Where(
                p => p.FirstName == player.FirstName
                && p.LastName == player.LastName);

            if (checkPlayer.Any())
            {
                throw new EntityAlreadyExistsException($"Player {player.FirstName} {player.LastName} already exists.");
            }

            var playerDbo = _mapper.Map<PlayerDbo>(player);
            _entities.Add(playerDbo);

            await SaveChangesAsync();
        }

        public async Task<IQueryable<Player>> All(
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties)
        {
            var query = _entities.AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            return _mapper.Map<IEnumerable<Player>>(query).AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetById(Guid id)
        {
            var playerDbo = _entities.Where(
                p => p.Id == id);

            return _mapper.Map<Player>(playerDbo.FirstOrDefault());
        }

        public void Delete(Player player)
        {
            var playerDbo = _mapper.Map<PlayerDbo>(player);
            _entities.Remove(playerDbo);
        }
    }
}
