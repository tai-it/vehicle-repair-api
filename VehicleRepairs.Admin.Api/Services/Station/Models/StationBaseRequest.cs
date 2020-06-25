namespace VehicleRepairs.Admin.Api.Services.Station.Models
{
    using VehicleRepairs.Shared.Common;

    public class StationBaseRequest : BaseRequestModel
    {
        public string Vehicle { get; set; }

        public string ServiceName { get; set; }

        public bool HasAmbulatory { get; set; }
    }
}