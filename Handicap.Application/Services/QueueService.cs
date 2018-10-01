using Handicap.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handicap.Application.Services
{
    public class QueueService : IQueueService
    {
        private ICollection<Player> AlreadyPlayedQueue { get; set; }

        private ICollection<Player> _queue;
        private readonly Random _rnd;

        public QueueService()
        {
            AlreadyPlayedQueue = new List<Player>();
            _rnd = new Random();
        }

        public void Setup(ICollection<Player> queue)
        {
            _queue = queue;
        }

        public bool IsQueueingPossible()
        {
            return (_queue.Count + AlreadyPlayedQueue.Count >= 2);
        }

        public Player GetNextPlayer()
        {
            if (_queue.Any())
            {
                var player = QueuePlayer(_queue);
                ManageQueues();

                return player;
            }

            if (AlreadyPlayedQueue.Any())
            {
                var player = QueuePlayer(AlreadyPlayedQueue);
                ManageQueues();

                return player;
            }

            return null;
        }

        private Player QueuePlayer(ICollection<Player> queue)
        {
            int index = _rnd.Next(0, queue.Count());
            var player = queue.ElementAt(index);
            queue.Remove(player);
            return player;
        }

        private void ManageQueues()
        {
            if (!_queue.Any() && AlreadyPlayedQueue.Any())
            {
                _queue = AlreadyPlayedQueue;
                AlreadyPlayedQueue.Clear();
            }
        }
    }
}
