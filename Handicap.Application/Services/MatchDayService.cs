using Handicap.Application.Interfaces;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public class MatchDayService : IMatchDayService
    {
        private readonly IMatchDayRepository _matchDayRepository;

        public MatchDayService(IMatchDayRepository matchDayRepository)
        {
            _matchDayRepository = matchDayRepository;
        }

        public async Task<MatchDay> AddPlayer(MatchDay matchDay, Player player)
        {
            matchDay.Players.Add(player);
            await _matchDayRepository.SaveChangesAsync();

            return matchDay;
        }

        public async Task<MatchDay> CreateMatchDay()
        {
            var matchDay = new MatchDay();
            await _matchDayRepository.Insert(matchDay);

            return matchDay;
        }

        public async Task<MatchDay> GetById(string id)
        {
            var matchDay = await _matchDayRepository.GetById(id);

            return matchDay;
        }
    }
}
