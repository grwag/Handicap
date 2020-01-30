using Handicap.Domain.Models;
using System;

namespace Handicap.Application.Services
{
    public class HandicapCalculator : IHandicapCalculator
    {
        public int Calculate(int handicap, int gameType)
        {
            var max = (double)gameType;
            var adjustment = ((max / 100) * handicap);

            var adjustedValue = (double)(max - adjustment);

            return (int)Math.Ceiling(adjustedValue);
        }
    }
}
