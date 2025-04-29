using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;     // default için 500          
            var errorMessage = "An unexpected error occurred.";

            if (ex is ArgumentNullException)
            {
                statusCode = HttpStatusCode.BadRequest;                         // 400
                errorMessage = "Eksik veya yanlış veri gönderildi.";
            }
            else if(ex is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;                       // 401
                errorMessage = "Yetkisiz erişim.";
            }
            else if(ex is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;                           // 404
                errorMessage = "İstenilen veri bulunamadı";
            }

            var errorResponse = new
            {
                StatusCode = (int)statusCode,
                ErrorMessage = errorMessage,
                ErrorDetail = ex.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
