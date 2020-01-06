using Handicap.Domain.Models;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public interface IHandicapUpdateService
    {
        Game UpdatePlayerHandicap(Game game);
        Task<MatchDay> UpdateMatchDayHandicaps(MatchDay matchDay);
    }
}