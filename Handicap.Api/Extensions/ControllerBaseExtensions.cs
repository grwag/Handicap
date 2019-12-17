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

            var sub = controller.User.Claims.FirstOrDefault(c => c.Type == "sub");

            if(sub == null)
            {
                return string.Empty;
            }

            return sub.Value ?? string.Empty;
        }
    }
}
