using VehicleRepairs.Api.Services.Ordering.Models;
using VehicleRepairs.Database.Domain.Entities;
using VehicleRepairs.Shared.Common;

namespace VehicleRepairs.Api.Services.Messaging.Models
{
    public class NotificationDetailViewModel : NotificationBaseViewModel
    {
        public NotificationDetailViewModel()
        {
        }

        public NotificationDetailViewModel(Notification notification, Order order) : base(notification)
        {
            if (notification != null)
            {
                if (notification.Type == CommonConstants.NotificationTypes.ORDER_TRACKING)
                {
                    Order = new OrderDetailViewModel(order);
                }
            }
        }

        public OrderDetailViewModel Order { get; set; }
    }
}
