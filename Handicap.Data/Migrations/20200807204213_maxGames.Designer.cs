﻿// <auto-generated />
using System;
using Handicap.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Handicap.Data.Migrations
{
    [DbContext(typeof(HandicapContext))]
    [Migration("20200807204213_maxGames")]
    partial class maxGames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Handicap.Domain.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MatchDayId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PlayerOneId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("PlayerOnePoints")
                        .HasColumnType("int");

                    b.Property<int>("PlayerOneRequiredPoints")
                        .HasColumnType("int");

                    b.Property<string>("PlayerTwoId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("PlayerTwoPoints")
                        .HasColumnType("int");

                    b.Property<int>("PlayerTwoRequiredPoints")
                        .HasColumnType("int");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchDayId");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Handicap.Domain.Models.HandicapConfiguration", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("EightBallMax")
                        .HasColumnType("int");

                    b.Property<int>("NineBallMax")
                        .HasColumnType("int");

                    b.Property<int>("StraigntPoolMax")
                        .HasColumnType("int");

                    b.Property<int>("TenBallMax")
                        .HasColumnType("int");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("UpdatePlayersImmediately")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("HandicapConfigurations");
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDay", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("MatchDays");
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDayPlayer", b =>
                {
                    b.Property<string>("MatchDayId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PlayerId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("MatchDayId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchDayPlayers");
                });

            modelBuilder.Entity("Handicap.Domain.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Handicap")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Handicap.Domain.Models.Game", b =>
                {
                    b.HasOne("Handicap.Domain.Models.MatchDay", null)
                        .WithMany("Games")
                        .HasForeignKey("MatchDayId");

                    b.HasOne("Handicap.Domain.Models.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Handicap.Domain.Models.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDayPlayer", b =>
                {
                    b.HasOne("Handicap.Domain.Models.MatchDay", "MatchDay")
                        .WithMany("MatchDayPlayers")
                        .HasForeignKey("MatchDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Handicap.Domain.Models.Player", "Player")
                        .WithMany("MatchDayPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
