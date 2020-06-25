namespace VehicleRepairs.Admin.Api.Services.Messaging.Models
{
    using System;
    using VehicleRepairs.Database.Domain.Entities;

    public class NotificationBaseViewModel
    {
        public NotificationBaseViewModel()
        {
        }

        public NotificationBaseViewModel(Notification notification)
        {
            if (notification != null)
            {
                Id = notification.Id;
                Title = notification.Title;
                Body = notification.Body;
                Type = notification.Type;
                IsSeen = notification.IsSeen;
                IsSent = notification.IsSent;
                CreatedOn = notification.CreatedOn;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Type { get; set; }

        public bool IsSeen { get; set; }

        public bool IsSent { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
