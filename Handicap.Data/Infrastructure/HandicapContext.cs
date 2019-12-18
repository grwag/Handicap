﻿using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Data.Infrastructure
{
    public class HandicapContext : DbContext, IHandicapContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<MatchDay> MatchDays { get; set; }

        public HandicapContext(DbContextOptions<HandicapContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
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

            //builder.Entity<MatchDayGame>(ConfigureMatchDayGames);
            //builder.Entity<MatchDayPlayer>(ConfigureMatchDayPlayers);
        }

        //private void ConfigureMatchDayGames(EntityTypeBuilder<MatchDayGame> config)
        //{
        //    config.HasKey(x => new
        //    {
        //        x.GameId,
        //        x.MatchDayId
        //    });

        //    config.HasOne(x => x.MatchDay)
        //        .WithMany(x => x.Games)
        //        .HasForeignKey(x => x.MatchDayId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //}

        //private void ConfigureMatchDayPlayers(EntityTypeBuilder<MatchDayPlayer> config)
        //{
        //    config.HasKey(x => new
        //    {
        //        x.PlayerId,
        //        x.MatchDayId
        //    });

        //    config.HasOne(x => x.MatchDay)
        //        .WithMany(x => x.MatchDayPlayers)
        //        .HasForeignKey(x => x.MatchDayId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //}
    }
}
