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
        public void ShouldAssertConfigurationIsValid()
        {
            var mapper = this.provider.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
