using Handicap.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Handicap.Api.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;

        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }
        }

        private static Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            var responseCode = HttpStatusCode.InternalServerError;

            if (ex is EntityNotFoundException)
            {
                responseCode = HttpStatusCode.NotFound;
            }
            else if (ex is EntityAlreadyExistsException)
            {
                responseCode = HttpStatusCode.Conflict;
            }

            var result = JsonConvert.SerializeObject(
                new
                {
                    errors = new[]
                    {
                        $"Exception: {ex.GetType()}",
                        $"Message: {ex.Message}"
                    }
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;

            return context.Response.WriteAsync(result);
        }
    }
}
