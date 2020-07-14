namespace VehicleRepairs.Api.Services.Messaging
{
    using MediatR;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Shared.Common;

    public class NotificationSendByUserIdsRequest : IRequest<ResponseModel>
    {
        [Required]
        public List<string> Ids { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string Data { get; set; }
    }
}
