namespace VehicleRepairs.Admin.Api.Services.Station
{
    using MediatR;
    using VehicleRepairs.Admin.Api.Services.Station.Models;
    using VehicleRepairs.Shared.Common;

    public class StationPagedListRequest : StationBaseRequest, IRequest<PagedList<StationDetailViewModel>>
    {
    }
}
