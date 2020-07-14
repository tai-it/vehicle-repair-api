namespace VehicleRepairs.Admin.Api.Services.Messaging.Models
{
    using VehicleRepairs.Admin.Api.Services.Ordering.Models;
    using VehicleRepairs.Database.Domain.Entities;

    public class NotificationDetailViewModel : NotificationBaseViewModel
    {
        public NotificationDetailViewModel()
        {
        }

        public NotificationDetailViewModel(Notification notification) : base(notification)
        {
            //Order = new OrderDetailViewModel(notification.Order);
        }

        public OrderDetailViewModel Order { get; set; }
    }
}
