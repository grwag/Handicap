using Handicap.Domain.Models;
using System;

namespace Handicap.Domain.Dbo
{
    public class GameDbo : BaseEntity
    {
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
        public GameType Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public int Table { get; set; }
    }
}