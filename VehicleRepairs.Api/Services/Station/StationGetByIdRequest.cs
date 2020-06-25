namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using VehicleRepairs.Shared.Common;

    public class StationGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
