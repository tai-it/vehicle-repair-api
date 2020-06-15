namespace VehicleRepairs.Api.Services.Station.Models
{
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationBaseRequest : BaseRequestModel
    {
        public string Vehicle { get; set; }

        public string ServiceName { get; set; }

        public bool HasAmbulatory { get; set; }
    }
}