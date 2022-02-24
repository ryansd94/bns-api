using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Application.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
