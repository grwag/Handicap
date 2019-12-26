using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class HandicapConfigurationRepository : IHandicapConfigurationRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<HandicapConfiguration> _configs;

        public HandicapConfigurationRepository(HandicapContext context)
        {
            _context = context;
            _configs = _context.Set<HandicapConfiguration>();
        }

        public async Task<HandicapConfiguration> GetConfig(string tenantId)
        {
            var config = await _configs.FirstOrDefaultAsync(c => c.TenantId == tenantId);
            if (config == null)
            {
                config = new HandicapConfiguration();
                config.TenantId = tenantId;

                _configs.Add(config);
                await _context.SaveChangesAsync();
            }

            return config;
        }

        public async Task<HandicapConfiguration> UpdateConfig(HandicapConfiguration config)
        {
            _context.Update(config);
            await _context.SaveChangesAsync();

            return config;
        }

        public async Task<HandicapConfiguration> ResetToDefaults(HandicapConfiguration config)
        {
            config.UpdatePlayersImmediately = true;

            _context.Update(config);
            await _context.SaveChangesAsync();

            return config;
        }
    }
}
