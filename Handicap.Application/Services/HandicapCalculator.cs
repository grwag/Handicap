using System;
using System.Collections.Generic;
using System.Text;
using Handicap.Application.Entities;

namespace Handicap.Application.Services
{
    public class HandicapCalculator : IHandicapCalculator
    {
        public int Calculate(int handicap, GameType gameType)
        {
            var max = 0;
            switch (gameType)
            {
                case GameType.EIGHTBALL:
                    max = 7;
                    break;
                case GameType.NINEBALL:
                    max = 9;
                    break;
                case GameType.TENBALL:
                    max = 8;
                    break;
                case GameType.STRAIGHTPOOL:
                    max = 100;
                    break;
            }

            return (int)Math.Ceiling((double)(max - ((max / 100) * handicap)));
        }
    }
}
