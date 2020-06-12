namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class OrderGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
