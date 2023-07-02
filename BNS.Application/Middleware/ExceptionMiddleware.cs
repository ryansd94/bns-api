
using BNS.Domain;
using BNS.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var result =new ApiResult<Guid> { title = error?.Message };
                result.errorCode = EErrorCode.Failed.ToString();
                result.title = error?.Message;
                switch (error)
                {
                    case AppException e:
                        // custom application error
                        result.status = HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        result.status = HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        result.status = HttpStatusCode.ExpectationFailed;
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
