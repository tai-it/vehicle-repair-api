namespace VehicleRepairs.Admin.Api.Infrastructure.Filters
{
    using System;

    public class JsonErrorResponse
    {
        public string[] Messages { get; set; } = Array.Empty<string>();

        public object DeveloperMessage { get; set; } = new object();
    }
}
