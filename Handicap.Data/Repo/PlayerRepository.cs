using AutoMapper;
using Handicap.Data.Exceptions;
using Handicap.Data.Infrastructure;
using Handicap.Data.Paging;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Handicap.Dto.Response.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public async Task<Player> Insert(Player player)
        {
            var playerDbo = _mapper.Map<Player, PlayerDbo>(player);

            if (_entities.Find(player.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Player '{player.FirstName} {player.LastName}' already exists.");
            }

            _entities.Add(playerDbo);
            await SaveChangesAsync();

            return _mapper.Map<Player>(playerDbo);
        }

        public async Task<PagedList<Player>> FindAsync<P>(
            Expression<Func<Player, bool>> expression,
            Expression<Func<Player, P>> expressionProperty,
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties)
        {
            var query = _mapper.Map<IEnumerable<Player>>(_entities).AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (desc)
            {
                query = query.OrderByDescending(expressionProperty);
            }
            else
            {
                query = query.OrderBy(expressionProperty);
            }

            return PagedList<Player>.Create(query, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
