namespace VehicleRepairs.Admin.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Admin.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class GetStationOrdersRequest : BaseRequestModel, IRequest<PagedList<OrderDetailViewModel>>
    {
        public Guid Id { get; set; }
    }
}
