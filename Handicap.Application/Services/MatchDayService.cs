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
        //private readonly IRepository<MatchDay> _matchDayRepository;

        //public MatchDayService(IRepository<MatchDay> matchDayRepository)
        //{
        //    _matchDayRepository = matchDayRepository;
        //}

        //public async Task<MatchDay> CreateMatchDay(int numberOfTables)
        //{
        //    var tables = new bool[numberOfTables];
        //    for(var i = 0; i < tables.Length; i++)
        //    {
        //        tables[i] = true;
        //    }

        //    var md = new MatchDay()
        //    {
        //        Players = new List<Player>(),
        //        PriorityQueue = new List<Player>(),
        //        Queue = new List<Player>(),
        //        Games = new List<Game>(),
        //        Tables = tables
        //    };

        //    _matchDayRepository.Insert(md);
        //    await _matchDayRepository.SaveChangesAsync();

        //    return md;
        //}
    }
}
