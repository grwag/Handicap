using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using AutoMapper;
using System;
using Handicap.Dto.Request;
using Handicap.Domain.Models;
using FluentAssertions;
using Handicap.Dto.Response;

namespace Handicap.Mapping.Tests
{
    [TestFixture]
    [Category("Mapping")]
    public class MapperTests
    {
        private readonly IServiceProvider provider;
        private readonly Guid playerOneId = Guid.Parse("{9E923FF8-1EFE-4E13-B4CE-2BF3D3260244}");
        private readonly Guid playerTwoId = Guid.Parse("{9E923FF8-1EFE-4E13-B4CE-2BF3D3260244}");

        public MapperTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
            this.provider = services.BuildServiceProvider();
        }

        [Test]
        public void PlayerRequest_MapsTo_Player()
        {
            var playerRequest = new PlayerRequest()
            {
                FirstName = "hans",
                LastName = "maulwurf",
                Handicap = 55
            };

            var mapper = this.provider.GetService<IMapper>();

            var mappedDomainPlayer = mapper.Map<PlayerRequest, Player>(playerRequest);

            mappedDomainPlayer.FirstName.Should().BeEquivalentTo(playerRequest.FirstName);
            mappedDomainPlayer.LastName.Should().BeEquivalentTo(playerRequest.LastName);
            mappedDomainPlayer.Handicap.Should().Be(playerRequest.Handicap);
        }

        [Test]
        public void Player_MapsTo_PlayerResponse()
        {
            var player = new Player()
            {
                Id = playerOneId,
                FirstName = "hans",
                LastName = "maulwurf",
                Handicap = 55
            };

            var mapper = this.provider.GetService<IMapper>();

            var mappedPlayerResponse = mapper.Map<Player, PlayerResponse>(player);

            mappedPlayerResponse.Id.Should().Be(player.Id);
            mappedPlayerResponse.FirstName.Should().BeEquivalentTo(player.FirstName);
            mappedPlayerResponse.LastName.Should().BeEquivalentTo(player.LastName);
            mappedPlayerResponse.Handicap.Should().Be(player.Handicap);
        }

        //[Test]
        //public void Game_MapsTo_GameResponse()
        //{
        //    var game = new Game()
        //    {
        //        Date = DateTimeOffset.Now,
        //        Id = Guid.NewGuid(),
        //        PlayerOne = new Player()
        //        {
        //            Id = playerOneId,
        //            FirstName = "hans",
        //            LastName = "maulwurf",
        //            Handicap = 20
        //        },
        //        PlayerTwo = new Player()
        //        {
        //            Id = playerTwoId,
        //            FirstName = "bart",
        //            LastName = "simpson",
        //            Handicap = 50
        //        },
        //        PlayerOnePoints = 30,
        //        PlayerOneRequiredPoints = 30,
        //        PlayerTwoPoints = 35,
        //        PlayerTwoRequiredPoints = 55
        //    };

        //    var mapper = this.provider.GetService<IMapper>();

        //    var gameResponse = mapper.Map<GameResponse>(game);
        //}
    }
}
