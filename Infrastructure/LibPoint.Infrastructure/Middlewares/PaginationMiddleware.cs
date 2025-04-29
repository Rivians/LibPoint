using LibPoint.Domain.Models.Options;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Middlewares
{
    public class PaginationMiddleware
    {
        private readonly RequestDelegate _next;
        public PaginationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var paginationOptions = new PaginationOptions();

            if(httpContext.Request.Headers.TryGetValue("Page-Number", out var pageNumber) && int.TryParse(pageNumber, out int integerPageNumber))
            {
                paginationOptions.PageNumber = integerPageNumber;
            }

            if(httpContext.Request.Headers.TryGetValue("Page-Size", out var pageSize) && int.TryParse(pageSize, out int integerPageSize))
            {
                paginationOptions.PageSize = integerPageSize;
            }

            httpContext.Items["PaginationOptions"] = paginationOptions;

            await _next(httpContext); 
        }
    }
}
