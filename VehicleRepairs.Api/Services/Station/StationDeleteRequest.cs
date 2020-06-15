namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
