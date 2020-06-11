namespace VehicleRepairs.Api.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System.Net;
    using VehicleRepairs.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Api.Infrastructure.Exceptions;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            this.logger.Error(
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(DomainException))
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { context.Exception.Message },
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occurred. Try it again." },
                };

                if (this.env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.ExceptionHandled = true;
        }
    }
}
