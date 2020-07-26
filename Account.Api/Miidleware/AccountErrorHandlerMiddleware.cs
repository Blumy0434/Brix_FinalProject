using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Account.WebApi.Miidleware
{
    public class AccountErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public AccountErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context )
        {
            try
            {
                await _next(context);                
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            
            if (ex is DuplicateEmailException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(new { error = false });
                return context.Response.WriteAsync(result);
            }
            if (ex is CustomerNotFoundException)
            {                
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var result = JsonConvert.SerializeObject(new { error = "Password or Email are not correct. try again!" });
                return context.Response.WriteAsync(result);                
            }
            if (ex is AccountNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var result = JsonConvert.SerializeObject(new { error = "There isn't such account!"});
                return context.Response.WriteAsync(result);
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("Internal server Error - 500");
        }
    }
}
