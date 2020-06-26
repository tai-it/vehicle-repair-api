namespace VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics
{
    using MediatR;
    using VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics.Models;
    using VehicleRepairs.Shared.Common;

    public class GetStationsStatisticRequest : BaseRequestModel, IRequest<PagedList<StationStatisticViewModel>>
    {
    }
}
