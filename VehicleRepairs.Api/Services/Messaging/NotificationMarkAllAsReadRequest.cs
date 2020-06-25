namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Shared.Common;

    public class NotificationMarkAllAsReadRequest : IRequest<ResponseModel>
    {
        public string PhoneNumber { get; set; }
    }
}
