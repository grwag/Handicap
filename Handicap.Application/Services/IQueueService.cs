using System.Collections.Generic;
using Handicap.Application.Entities;

namespace Handicap.Application.Services
{
    public interface IQueueService
    {
        Player GetNextPlayer();
        void Setup(ICollection<Player> queue);
        bool IsQueueingPossible();
    }
}