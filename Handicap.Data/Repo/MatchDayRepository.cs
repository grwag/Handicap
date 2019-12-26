using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class MatchDayRepository : IMatchDayRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<MatchDay> _matchDays;

        private readonly ILogger<MatchDayRepository> _logger;

        public MatchDayRepository(HandicapContext context,
            ILogger<MatchDayRepository> logger)
        {
            _context = context;
            _logger = logger;

            _matchDays = context.Set<MatchDay>();
        }

        public async Task<IQueryable<MatchDay>> Find(
            Expression<Func<MatchDay, bool>> expression,
            params string[] navigationProperties)
        {
            var matchDays = _context.MatchDays.AsNoTracking().AsQueryable();

            var query = matchDays
                //.Include(md => md.MatchDayPlayers)
                //.ThenInclude(mp => mp.Player)
                .AsNoTracking()
                .AsQueryable()
                ;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query;
        }

        public void Delete(MatchDay matchDay)
        {
            throw new NotImplementedException();
        }

        public async Task<MatchDay> GetById(string id)
        {
            var query = _matchDays
                .AsQueryable()
                .AsNoTracking();

            query = query
                .Include($"{nameof(MatchDay.Games)}")
                .Include($"{nameof(MatchDay.MatchDayPlayers)}")
                ;

            var matchDay = query.SingleOrDefault(md => md.Id == id);

            if(matchDay == null)
            {
                throw new EntityNotFoundException($"MatchDay with id {id} not found.");
            }

            return matchDay;
        }

        public async Task Insert(MatchDay matchDay)
        {
            if(_matchDays.SingleOrDefault(md => md.Id == matchDay.Id) != null)
            {
                throw new EntityAlreadyExistsException($"MatchDay with id {matchDay.Id} already exists.");
            }

            _matchDays.Add(matchDay);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<MatchDay> Update(MatchDay matchDay)
        {
            if(matchDay == null)
            {
                throw new EntityNotFoundException($"Matchday with id {matchDay.Id} does not exist.");
            }

            _context.Update(matchDay);
            return matchDay;
        }

        public async Task<MatchDay> UpdateMatchDayPlayers(MatchDay matchDay)
        {
            if (matchDay == null)
            {
                throw new EntityNotFoundException($"Matchday with id {matchDay.Id} does not exist.");
            }

            var matchDayPlayers = _context.MatchDayPlayers
                .Where(md => md.MatchDayId == matchDay.Id)
                .AsNoTracking()
                .ToList();

            foreach (var matchDayPlayer in matchDayPlayers)
            {
                var existingMatchDayPlayer = matchDay.MatchDayPlayers
                    .FirstOrDefault(md => md.PlayerId == matchDayPlayer.PlayerId);

                if (existingMatchDayPlayer != null)
                {
                    continue;
                }
                _context.Remove(matchDayPlayer);
            }

            _context.Update(matchDay);
            return matchDay;
        }
    }
}
