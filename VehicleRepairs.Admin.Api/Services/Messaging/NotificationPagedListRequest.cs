namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Admin.Api.Services.Messaging.Models;
    using VehicleRepairs.Shared.Common;

    public class NotificationPagedListRequest : BaseRequestModel, IRequest<PagedList<NotificationBaseViewModel>>
    {

    }
}
