using VehicleRepairs.Api.Services.Ordering.Models;
using VehicleRepairs.Database.Domain.Entities;

namespace VehicleRepairs.Api.Services.Messaging.Models
{
    public class NotificationDetailViewModel : NotificationBaseViewModel
    {
        public NotificationDetailViewModel()
        {
        }

        public NotificationDetailViewModel(Notification notification) : base(notification)
        {
            Order = new OrderDetailViewModel(notification.Order);
        }

        public OrderDetailViewModel Order { get; set; }
    }
}
