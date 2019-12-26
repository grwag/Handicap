using Handicap.Domain.Models;
using System.Threading.Tasks;

namespace Handicap.Application.Interfaces
{
    public interface IHandicapConfigurationRepository
    {
        Task<HandicapConfiguration> GetConfig(string tenantId);
        Task<HandicapConfiguration> UpdateConfig(HandicapConfiguration config);
        Task<HandicapConfiguration> ResetToDefaults(HandicapConfiguration config);
    }
}