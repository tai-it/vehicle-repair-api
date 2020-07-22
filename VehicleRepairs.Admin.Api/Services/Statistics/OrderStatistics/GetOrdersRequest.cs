namespace VehicleRepairs.Admin.Api.Services.Statistics.OrderStatistics
{
    using MediatR;
    using System;
    using VehicleRepairs.Admin.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    public class GetOrdersRequest : BaseRequestModel, IRequest<PagedList<OrderDetailViewModel>>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
