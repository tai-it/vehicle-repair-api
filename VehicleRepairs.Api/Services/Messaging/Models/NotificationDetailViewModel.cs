using VehicleRepairs.Api.Domain.Entities;
using VehicleRepairs.Api.Services.Ordering.Models;

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
