using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Api.Extensions;
using Handicap.Application.Services;
using Handicap.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IHandicapConfigurationService _configService;

        public ConfigController(IHandicapConfigurationService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tenantId = this.GetTenantId();
            var config = await _configService.Get(tenantId);

            return Ok(config);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]HandicapConfigurationUpdate configUpdate)
        {
            var tenantId = this.GetTenantId();
            var config = await _configService.Update(configUpdate, tenantId);

            return Ok(config);
        }

        [HttpPut("reset")]
        public async Task<IActionResult> ResetToDefaults()
        {
            var tenantId = this.GetTenantId();
            var config = await _configService.ResetToDefaults(tenantId);

            return Ok(config);
        }
    }
}