namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class OrderEditRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }

        public string Status { get; set; }
    }
}
