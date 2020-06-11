namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
