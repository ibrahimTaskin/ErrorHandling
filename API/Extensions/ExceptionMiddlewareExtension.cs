using API.Middlewares.CustomExceptionMiddleware;
using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Extensions
{
    /// <summary>
    /// Core'un kendi ExceptionHandler'ını kullanarak yaptığımız extension
    /// </summary>
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Product> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Dönüşte HttpStatus bir Response istiyoruz
                    context.Response.ContentType = "application/json"; // Json olacak

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.LogError($"Somethings went wrong {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            Message = "Internal Service Error",
                            StatusCode = context.Response.StatusCode
                        }
                        .ToString()
                        );
                    }
                });
            });
        }

        /// <summary>
        /// Kendi Custom Exception Middleware'i register ettik.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(); // Middleware'ımızı register ettik.
        }
    }
}
