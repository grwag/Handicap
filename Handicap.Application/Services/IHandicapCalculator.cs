using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IHandicapCalculator
    {
        int Calculate(int handicap, int gameType);
    }
}