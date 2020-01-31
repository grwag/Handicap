using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Api.Extensions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ClientConfigController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var config = new ClientConfig
            {
                ClientId = System.Environment.GetEnvironmentVariable("OAUTH_CLIENT_ID")
                    ?? "2772bca8-6909-4190-8a9f-0fa448561eb9",
                ClientSecret = System.Environment.GetEnvironmentVariable("OAUTH_CLIENT_SECRET")
                    ?? "secret",
                PostLogoutRedirectUri = System.Environment.GetEnvironmentVariable("OAUTH_POST_LOGOUT_REDIRECT_URI")
                    ?? "https://localhost:5001",
                Issuer = System.Environment.GetEnvironmentVariable("OAUTH_ISSUER")
                    ?? "https://id.greshawag.com",
                Scope = System.Environment.GetEnvironmentVariable("OAUTH_SCOPE")
                    ?? "read read_write openid profile email offline_access",
                ResponseType = System.Environment.GetEnvironmentVariable("OAUTH_RESPONSE_TYPE")
                    ?? "code",
                RedirectUri = System.Environment.GetEnvironmentVariable("OAUTH_REDIRECT_URI")
                    ?? "https://localhost:5001"
            };

            return Ok(config);
        }
    }
}