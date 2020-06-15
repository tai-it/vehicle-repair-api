namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Station.Models;

    public class StationPagedListRequest : StationBaseRequest, IRequest<PagedList<StationBaseViewModel>>
    {
    }
}
