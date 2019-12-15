using Handicap.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Data.Infrastructure
{
    public class HandicapContext : DbContext, IHandicapContext
    {
        public DbSet<PlayerDbo> Players { get; set; }
        public DbSet<GameDbo> Games { get; set; }
        public DbSet<MatchDayDbo> MatchDays { get; set; }

        public HandicapContext(DbContextOptions<HandicapContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<PlayerDbo>().HasData(
                new PlayerDbo
                {
                    Id = "1",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "alf",
                    LastName = "ralf",
                    Handicap = 65
                },
                new PlayerDbo
                {
                    Id = "2",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "hans",
                    LastName = "maulwurf",
                    Handicap = 35
                },
                new PlayerDbo
                {
                    Id = "3",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "karl",
                    LastName = "klammer",
                    Handicap = 30
                },
                new PlayerDbo
                {
                    Id = "4",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "bart",
                    LastName = "simpson",
                    Handicap = 55
                },
                new PlayerDbo
                {
                    Id = "5",
                    TenantId = "def",
                    FirstName = "nasen",
                    LastName = "baer",
                    Handicap = 25
                },
                new PlayerDbo
                {
                    Id = "6",
                    TenantId = "def",
                    FirstName = "eier",
                    LastName = "kopf",
                    Handicap = 5
                },
                new PlayerDbo
                {
                    Id = "7",
                    TenantId = "def",
                    FirstName = "rudi",
                    LastName = "rakete",
                    Handicap = 30
                },
                new PlayerDbo
                {
                    Id = "8",
                    TenantId = "def",
                    FirstName = "homer",
                    LastName = "simpson",
                    Handicap = 55
                }
                );
        }
    }
}
