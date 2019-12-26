using Handicap.Domain.Models;
using Handicap.Dto.Request;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public interface IHandicapConfigurationService
    {
        Task<HandicapConfiguration> Get(string tenantId);
        Task<HandicapConfiguration> Update(HandicapConfigurationUpdate configUpdate, string tenantId);
        Task<HandicapConfiguration> ResetToDefaults(string tenantId);
    }
}