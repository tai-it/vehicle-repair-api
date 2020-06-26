namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using VehicleRepairs.Shared.Common;

    public class SendNotificationRequest : IRequest<ResponseModel>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string UserId { get; set; }
    }
}
