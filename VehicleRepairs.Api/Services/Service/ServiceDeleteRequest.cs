using MediatR;
namespace VehicleRepairs.Api.Services.Service
{
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
