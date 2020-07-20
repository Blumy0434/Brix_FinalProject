using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
