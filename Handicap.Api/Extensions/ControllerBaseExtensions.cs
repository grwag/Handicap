using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicap.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetTenantId(this ControllerBase controller)
        {
            if(controller.User == null)
            {
                return string.Empty;
            }

            return controller.User.Claims.First(c => c.Type == "sub").Value ?? string.Empty;
        }
    }
}
