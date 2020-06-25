using VehicleRepairs.Shared.Common;

namespace VehicleRepairs.Api.Services.Station.Models
{

    public class StationBaseRequest : BaseRequestModel
    {
        public string Vehicle { get; set; }

        public string ServiceName { get; set; }

        public bool HasAmbulatory { get; set; }
    }
}