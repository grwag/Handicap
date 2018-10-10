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
        private readonly Guid playerId = Guid.Parse("{9E923FF8-1EFE-4E13-B4CE-2BF3D3260244}");

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
                Id = playerId,
                FirstName = "hans",
                LastName = "maulwurf",
                Handicap = 55,
                HasFinishedMatchDay = false
            };

            var mapper = this.provider.GetService<IMapper>();

            var mappedPlayerResponse = mapper.Map<Player, PlayerResponse>(player);

            mappedPlayerResponse.Id.Should().Be(player.Id);
            mappedPlayerResponse.FirstName.Should().BeEquivalentTo(player.FirstName);
            mappedPlayerResponse.LastName.Should().BeEquivalentTo(player.LastName);
            mappedPlayerResponse.Handicap.Should().Be(player.Handicap);
        }
    }
}
