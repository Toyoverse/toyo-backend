using BackendToyo.Exceptions;
using BackendToyo.Models.ResponseEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BackendToyo
{
    public static class ServiceExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => 
            {
                error.Run(async context => 
                {
                    int statusCode;                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    string message = contextFeature.Error.Message;
                    switch(contextFeature.Error)
                    {
                        case InvalidPasswordException:
                        case InvalidCredentialsException:
                        case InvalidTokenException:
                            statusCode = 401;
                            break;
                        case NotFoundException:
                            statusCode = 404;
                            break;
                        default:
                            statusCode = 500;
                            message = "Internal Error";
                            break;
                    }
                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        new ResponseStatusEntity(statusCode, message).ToJson());             
                });
            });
        }
    }
}