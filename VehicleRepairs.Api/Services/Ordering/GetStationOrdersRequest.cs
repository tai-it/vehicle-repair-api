namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class GetStationOrdersRequest : BaseRequestModel, IRequest<PagedList<OrderDetailViewModel>>
    {
        public Guid Id { get; set; }
    }
}
