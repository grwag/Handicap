using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IMatchDayService
    {
        Task<MatchDay> CreateMatchDay();
        Task<MatchDay> AddPlayer(MatchDay matchDay, Player player);
        Task<MatchDay> GetById(string id);
    }
}