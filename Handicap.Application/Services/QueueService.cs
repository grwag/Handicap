using Handicap.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Application.Services
{
    public class QueueService
    {
        private readonly MatchDay _matchDay;

        public QueueService(MatchDay matchDay)
        {
            _matchDay = matchDay;
        }

        public Player GetNextPlayer()
        {
            return null;
        }
    }
}
