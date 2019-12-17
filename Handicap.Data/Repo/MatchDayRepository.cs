using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Linq.Expressions;
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

        public async Task<IQueryable<MatchDay>> Find(
            Expression<Func<MatchDay, bool>> expression,
            params string[] navigationProperties)
        {
            var query = _matchDays
                .AsQueryable()
                .AsNoTracking()
                .Include(md => md.MatchDayGames)
                .Include(md => md.MatchDayPlayers);


            //foreach (var navigationProperty in navigationProperties)
            //{
            //    query = query.Include(navigationProperty);
            //}

            var domainQuery = query.ProjectTo<MatchDay>(_mapper.ConfigurationProvider, expression);

            return domainQuery;
        }

        public void Delete(MatchDay matchDay)
        {
            throw new NotImplementedException();
        }

        public async Task<MatchDay> GetById(string id)
        {
            var query = _matchDays.AsQueryable();
            query = query
                .Include($"{nameof(MatchDayDbo.MatchDayGames)}")
                .Include($"{nameof(MatchDayDbo.MatchDayPlayers)}")
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

        public async Task<MatchDay> Update(MatchDay matchDay)
        {
            var entity = _matchDays
                .Include(md => md.MatchDayGames)
                .Include(md => md.MatchDayPlayers)
                .SingleOrDefault(md => md.Id == matchDay.Id);

            if(entity == null)
            {
                throw new EntityNotFoundException($"Matchday with id {matchDay.Id} does not exist.");
            }

            var matchDayDbo = _mapper.Map<MatchDayDbo>(matchDay);
            foreach(var matchDayGame in matchDayDbo.MatchDayGames)
            {
                var gameExists = entity.MatchDayGames.Where(x => x.GameId == matchDayGame.GameId).SingleOrDefault();
                if (gameExists == null)
                {
                    _context.Attach(matchDayGame.Game.PlayerOne);
                    _context.Attach(matchDayGame.Game.PlayerTwo);
                    _context.Attach(matchDayGame.Game);
                    entity.MatchDayGames.Add(matchDayGame);
                }
            }

            foreach (var matchDayPlayer in matchDayDbo.MatchDayPlayers)
            {
                var playerExists = entity.MatchDayPlayers.Where(x => x.PlayerId == matchDayPlayer.PlayerId).SingleOrDefault();
                if (playerExists == null)
                {
                    entity.MatchDayPlayers.Add(matchDayPlayer);
                }
            }

            _context.Update(entity);

            await SaveChangesAsync();
            return _mapper.Map<MatchDay>(entity);
        }
    }
}
