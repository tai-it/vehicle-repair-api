namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using System;
    using VehicleRepairs.Shared.Common;

    public class OrderEditRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }
    }
}
