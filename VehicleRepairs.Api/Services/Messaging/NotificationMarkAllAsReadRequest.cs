namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class NotificationMarkAllAsReadRequest : IRequest<ResponseModel>
    {
        public string PhoneNumber { get; set; }
    }
}
