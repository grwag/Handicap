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

        public Player NextPlayer()
        {
            if (_queue.Any())
            {
                var player = TakePlayer(_queue);
                RefreshQueues();

                return player;
            }

            if (AlreadyPlayedQueue.Any())
            {
                var player = TakePlayer(AlreadyPlayedQueue);
                RefreshQueues();

                return player;
            }

            return null;
        }

        public void RequeuePlayer(Player player)
        {
            AlreadyPlayedQueue.Add(player);
            RefreshQueues();
        }

        public void AddPlayer(Player player)
        {
            _queue.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            if (_queue.Contains(player))
            {
                _queue.Remove(player);
            }

            if (AlreadyPlayedQueue.Contains(player))
            {
                AlreadyPlayedQueue.Remove(player);
            }
        }

        private Player TakePlayer(ICollection<Player> queue)
        {
            int index = _rnd.Next(0, queue.Count());
            var player = queue.ElementAt(index);
            queue.Remove(player);
            return player;
        }

        private void RefreshQueues()
        {
            if (!_queue.Any() && AlreadyPlayedQueue.Any())
            {
                _queue = AlreadyPlayedQueue;
                AlreadyPlayedQueue.Clear();
            }
        }
    }
}
