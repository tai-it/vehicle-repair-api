using VehicleRepairs.Shared.Common;

namespace VehicleRepairs.Admin.Api.Services.Service.Models
{

    public class ServiceBaseRequest : BaseRequestModel
    {
        public string Vehicle { get; set; }

        public bool IsDistinct { get; set; }
    }
}
