namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
