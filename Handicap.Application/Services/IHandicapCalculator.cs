using Handicap.Application.Entities;

namespace Handicap.Application.Services
{
    public interface IHandicapCalculator
    {
        int Calculate(int handicap, GameType gameType);
    }
}