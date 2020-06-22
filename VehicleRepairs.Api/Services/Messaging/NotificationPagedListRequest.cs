namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Messaging.Models;

    public class NotificationPagedListRequest : BaseRequestModel, IRequest<PagedList<NotificationDetailViewModel>>
    {
        public string PhoneNumber { get; set; }
    }
}
