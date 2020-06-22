namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using System;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class NotificationGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
