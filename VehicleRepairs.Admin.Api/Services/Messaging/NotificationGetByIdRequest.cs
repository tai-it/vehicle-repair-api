namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using System;
    using VehicleRepairs.Shared.Common;

    public class NotificationGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
