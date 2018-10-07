using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Services
{
    public interface IMatchDayService
    {
        Task<MatchDay> CreateMatchDay(int numberOfTables);
    }
}