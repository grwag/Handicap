using AutoMapper;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class MatchDayRepository : IMatchDayRepository
    {
        private readonly HandicapContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<MatchDayDbo> _matchDays;

        private readonly ILogger<MatchDayRepository> _logger;

        public MatchDayRepository(HandicapContext context,
            IMapper mapper,
            ILogger<MatchDayRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

            _matchDays = context.Set<MatchDayDbo>();
        }

        public async Task<IQueryable<MatchDay>> All(params string[] navigationProperties)
        {
            var query = _matchDays.AsQueryable();

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return _mapper.Map<IEnumerable<MatchDay>>(query).AsQueryable();
        }

        public void Delete(MatchDay matchDay)
        {
            throw new NotImplementedException();
        }

        public async Task<MatchDay> GetById(string id)
        {
            var query = _matchDays.AsQueryable();
            query = query
                .Include($"{nameof(MatchDayDbo.Games)}")
                .Include($"{nameof(MatchDayDbo.Players)}")
                .Include($"{nameof(MatchDayDbo.Queue)}")
                .Include($"{nameof(MatchDayDbo.PriorityQueue)}")
                ;

            var matchDayDbo = query.SingleOrDefault(md => md.Id == id);

            if(matchDayDbo == null)
            {
                throw new EntityNotFoundException($"MatchDay with id {id} not found.");
            }

            return _mapper.Map<MatchDay>(matchDayDbo);
        }

        public async Task Insert(MatchDay matchDay)
        {
            if(_matchDays.SingleOrDefault(md => md.Id == matchDay.Id) != null)
            {
                throw new EntityAlreadyExistsException($"MatchDay with id {matchDay.Id} already exists.");
            }

            var matchDayDbo = _mapper.Map<MatchDayDbo>(matchDay);
            _matchDays.Add(matchDayDbo);

            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(MatchDay matchDay)
        {
            var matchDayDbo = _matchDays.SingleOrDefault(md => md.Id == matchDay.Id);

            if(matchDayDbo == null)
            {
                throw new EntityNotFoundException($"Matchday with id {matchDay.Id} does not exist.");
            }

            matchDayDbo.Games = _mapper.Map<ICollection<GameDbo>>(matchDay.Games);
            matchDayDbo.Players = _mapper.Map<ICollection<PlayerDbo>>(matchDay.Players);
            matchDayDbo.PriorityQueue = _mapper.Map<ICollection<PlayerDbo>>(matchDay.PriorityQueue);
            matchDayDbo.Queue = _mapper.Map<ICollection<PlayerDbo>>(matchDay.Queue);

            //_context.Entry(matchDayDbo).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}
