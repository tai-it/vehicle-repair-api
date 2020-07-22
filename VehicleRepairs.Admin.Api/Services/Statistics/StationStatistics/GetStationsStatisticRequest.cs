namespace VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics
{
    using MediatR;
    using System;
    using VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics.Models;
    using VehicleRepairs.Shared.Common;

    public class GetStationsStatisticRequest : BaseRequestModel, IRequest<PagedList<StationStatisticViewModel>>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
