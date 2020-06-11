namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
