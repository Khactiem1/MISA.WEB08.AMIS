using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MISA.WEB08.AMIS.Common.Enums;
using System;
using MISA.WEB08.AMIS.Common.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MISA.WEB08.AMIS.Common.Exceptions;
using MISA.WEB08.AMIS.Common.Result;

namespace MISA.WEB08.AMIS.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception.Message);
            var result = new ServiceResponse
            {
                Success = false,
                ErrorCode = MisaAmisErrorCode.Exception,
                Data = new MisaAmisErrorResult(
                    MisaAmisErrorCode.Exception,
                    exception.Message.ToString(),
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    context.TraceIdentifier
                )
            };
            var resultJson = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(resultJson);
        }
    }
}
