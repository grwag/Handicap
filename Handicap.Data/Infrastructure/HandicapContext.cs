using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handicap.Data.Infrastructure
{
    public class HandicapContext : DbContext, IHandicapContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<MatchDay> MatchDays { get; set; }
        public DbSet<MatchDayPlayer> MatchDayPlayers { get; set; }
        public DbSet<HandicapConfiguration> HandicapConfigurations { get; set; }

        public HandicapContext(DbContextOptions<HandicapContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HandicapConfiguration>().HasData(
                new HandicapConfiguration
                {
                    Id = "99",
                    TenantId = "",
                    UpdatePlayersImmediately = false
                });

            builder.Entity<Player>().HasData(
                new Player
                {
                    Id = "1",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "alf",
                    LastName = "ralf",
                    Handicap = 65
                },
                new Player
                {
                    Id = "2",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "hans",
                    LastName = "maulwurf",
                    Handicap = 35
                },
                new Player
                {
                    Id = "3",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "karl",
                    LastName = "klammer",
                    Handicap = 30
                },
                new Player
                {
                    Id = "4",
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    FirstName = "bart",
                    LastName = "simpson",
                    Handicap = 55
                },
                new Player
                {
                    Id = "5",
                    TenantId = "",
                    FirstName = "nasen",
                    LastName = "baer",
                    Handicap = 25
                },
                new Player
                {
                    Id = "6",
                    TenantId = "",
                    FirstName = "eier",
                    LastName = "kopf",
                    Handicap = 5
                },
                new Player
                {
                    Id = "7",
                    TenantId = "",
                    FirstName = "rudi",
                    LastName = "rakete",
                    Handicap = 30
                },
                new Player
                {
                    Id = "8",
                    TenantId = "",
                    FirstName = "homer",
                    LastName = "simpson",
                    Handicap = 55
                }
                );

            builder.Entity<Game>().HasData(
                new Game
                {
                    Id = "g1",
                    Date = DateTimeOffset.Now,
                    IsFinished = true,
                    MatchDayId = "m1",
                    PlayerOne = null,
                    PlayerOneId = "1",
                    PlayerOnePoints = 5,
                    PlayerOneRequiredPoints = 5,
                    PlayerTwo = null,
                    PlayerTwoId = "2",
                    PlayerTwoPoints = 0,
                    PlayerTwoRequiredPoints = 5,
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    Type = GameType.Eightball
                },
                new Game
                {
                    Id = "g2",
                    Date = DateTimeOffset.Now,
                    IsFinished = true,
                    MatchDayId = "m1",
                    PlayerOne = null,
                    PlayerOneId = "1",
                    PlayerOnePoints = 5,
                    PlayerOneRequiredPoints = 5,
                    PlayerTwo = null,
                    PlayerTwoId = "2",
                    PlayerTwoPoints = 0,
                    PlayerTwoRequiredPoints = 5,
                    TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb",
                    Type = GameType.Eightball
                });

            ConfigureMatchDayPlayers(builder);

            ConfigureGame(builder);
        }

        private void ConfigureMatchDayPlayers(ModelBuilder builder)
        {
            builder.Entity<MatchDayPlayer>()
                .HasKey(mp => new { mp.MatchDayId, mp.PlayerId });

            builder.Entity<MatchDayPlayer>()
                .HasOne(mp => mp.MatchDay)
                .WithMany(m => m.MatchDayPlayers)
                .HasForeignKey(mp => mp.MatchDayId);

            builder.Entity<MatchDayPlayer>()
                .HasOne(mp => mp.Player)
                .WithMany(p => p.MatchDayPlayers)
                .HasForeignKey(mp => mp.PlayerId);
        }

        private void ConfigureGame(ModelBuilder builder)
        {
            builder.Entity<Game>(b =>
            {
                b.HasOne("Handicap.Domain.Models.Player", "PlayerOne")
                .WithMany()
                .HasForeignKey("PlayerOneId")
                .OnDelete(DeleteBehavior.SetNull);

                b.HasOne("Handicap.Domain.Models.Player", "PlayerTwo")
                .WithMany()
                .HasForeignKey("PlayerTwoId")
                .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
