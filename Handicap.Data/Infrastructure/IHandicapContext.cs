using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Handicap.Data.Infrastructure
{
    public interface IHandicapContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<MatchDay> MatchDays { get; set; }
        DbSet<Player> Players { get; set; }
        int SaveChanges();
    }
}