using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public class HandicapConfigurationService : IHandicapConfigurationService
    {
        private readonly IHandicapConfigurationRepository _configRepo;
        private readonly IMatchDayRepository _matchDayRepository;

        public HandicapConfigurationService(
            IHandicapConfigurationRepository configRepo,
            IMatchDayRepository matchDayRepository
            )
        {
            _configRepo = configRepo;
            _matchDayRepository = matchDayRepository;
        }

        public async Task<HandicapConfiguration> Get(string tenantId)
        {
            var config = await _configRepo.GetConfig(tenantId);

            return config;
        }

        public async Task<HandicapConfiguration> Update(HandicapConfigurationUpdate configUpdate, string tenantId)
        {
            var config = await Get(tenantId);
            var unfinishedMatchDays = await _matchDayRepository.Find(md => md.TenantId == tenantId && !md.IsFinished);

            if(unfinishedMatchDays.Count() > 0)
            {
                throw new EntityClosedForUpdateException($"Cannot update settings. Youn have unfinished MatchDays.");
            }

            config.UpdatePlayersImmediately = configUpdate.UpdatePlayersImmediately;

            await _configRepo.UpdateConfig(config);

            return config;
        }

        public async Task<HandicapConfiguration> ResetToDefaults(string tenantId)
        {
            var config = await Get(tenantId);
            var unfinishedMatchDays = await _matchDayRepository.Find(md => md.TenantId == tenantId && !md.IsFinished);

            if (unfinishedMatchDays.Count() > 0)
            {
                throw new EntityClosedForUpdateException($"Cannot reset settings. Youn have unfinished MatchDays.");
            }

            await _configRepo.ResetToDefaults(config);

            return config;
        }
    }
}
