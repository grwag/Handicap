using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Data.Paging;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<PlayerDbo> _players;
        private readonly IMapper _mapper;

        public PlayerRepository(HandicapContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _players = context.Set<PlayerDbo>();
        }

        public async Task<Player> AddOrUpdate(Player player)
        {
            if (PlayerExists(player))
            {
                return await Update(player);
            }
            else
            {
                return await Add(player);
            }
        }

        public async Task<IQueryable<Player>> Find(Expression<Func<Player, bool>> expression = null)
        {
            var query = _players
                .AsQueryable()
                .AsNoTracking()
                .ProjectTo<Player>(_mapper.ConfigurationProvider);

            if (expression != null)
            {
                query = query
                    .Where(expression);
            }

            return query;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var playerDbo = _players.Where(p => p.Id == id).SingleOrDefault();

            if (playerDbo != null)
            {
                _players.Remove(playerDbo);
                await SaveChangesAsync();
            }
        }
        private async Task<Player> Add(Player player)
        {
            var playerDbo = _mapper.Map<PlayerDbo>(player);

            _players.Add(playerDbo);
            await SaveChangesAsync();

            return _mapper.Map<Player>(playerDbo);
        }

        private async Task<Player> Update(Player player)
        {
            var playerDbo = _mapper.Map<PlayerDbo>(player);

            _context.Update(playerDbo);
            await SaveChangesAsync();

            return _mapper.Map<Player>(playerDbo);
        }


        private bool PlayerExists(Player player)
        {
            var query = _players
                .AsNoTracking();

            if (query.Where(p => p.Id == player.Id).FirstOrDefault() != null) { return true; }

            var exists = query.Where(
                p => p.FirstName == player.FirstName
                && p.LastName == player.LastName)
                .FirstOrDefault();

            return exists != null;
        }
    }
}
