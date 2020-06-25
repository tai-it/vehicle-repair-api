namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Api.Services.Messaging.Models;
    using VehicleRepairs.Shared.Common;

    public class NotificationPagedListRequest : BaseRequestModel, IRequest<PagedList<NotificationDetailViewModel>>
    {
        public string PhoneNumber { get; set; }
    }
}
