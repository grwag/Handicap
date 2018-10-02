using Handicap.Domain.Models;
using System;

namespace Handicap.Application.Services
{
    public class HandicapCalculator : IHandicapCalculator
    {
        public int Calculate(int handicap, GameType gameType)
        {
            var max = (int)gameType;

            return (int)Math.Ceiling((double)(max - ((max / 100) * handicap)));
        }
    }
}
