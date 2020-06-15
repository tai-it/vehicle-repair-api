namespace VehicleRepairs.Api.Services.Service.Models
{
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceBaseRequest : BaseRequestModel
    {
        public string Vehicle { get; set; }

        public bool IsDistinct { get; set; }
    }
}
