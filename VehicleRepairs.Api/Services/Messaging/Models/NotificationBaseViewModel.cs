namespace VehicleRepairs.Api.Services.Messaging.Models
{
    using System;
    using VehicleRepairs.Api.Domain.Entities;

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
                Seen = notification.IsSeen;
                CreatedOn = notification.CreatedOn;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public bool Seen { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
