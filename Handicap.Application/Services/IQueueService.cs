using System.Collections.Generic;
using Handicap.Application.Entities;

namespace Handicap.Application.Services
{
    public interface IQueueService
    {
        Player NextPlayer();
        void Setup(ICollection<Player> queue);
        void RequeuePlayer(Player player);
        void AddPlayer(Player player);
        void RemovePlayer(Player player);
        bool IsQueueingPossible();
    }
}