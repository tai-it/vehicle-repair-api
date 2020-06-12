﻿namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class GetStationOrdersRequest : BaseRequestModel, IRequest<PagedList<OrderBaseViewModel>>
    {
        public Guid Id { get; set; }
    }
}