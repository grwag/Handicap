using Handicap.Domain.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Data.Infrastructure
{
    public class HandicapContext : DbContext
    {
        public DbSet<PlayerDbo> Players { get; set; }
        public DbSet<GameDbo> Games { get; set; }
        public DbSet<MatchDayDbo> MatchDays { get; set; }

        public HandicapContext(DbContextOptions<HandicapContext> options) : base(options)
        {
        }
    }
}
