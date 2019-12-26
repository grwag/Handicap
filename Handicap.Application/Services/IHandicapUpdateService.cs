using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IHandicapUpdateService
    {
        Game UpdatePlayerHandicap(Game game);
    }
}