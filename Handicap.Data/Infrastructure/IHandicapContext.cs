using Handicap.Dbo;
using Microsoft.EntityFrameworkCore;

namespace Handicap.Data.Infrastructure
{
    public interface IHandicapContext
    {
        DbSet<GameDbo> Games { get; set; }
        DbSet<MatchDayDbo> MatchDays { get; set; }
        DbSet<PlayerDbo> Players { get; set; }
        int SaveChanges();
    }
}