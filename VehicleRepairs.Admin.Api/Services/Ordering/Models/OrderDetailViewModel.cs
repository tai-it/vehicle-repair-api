namespace VehicleRepairs.Admin.Api.Services.Ordering.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Admin.Api.Services.Service.Models;
    using VehicleRepairs.Admin.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Entities;

    public class OrderDetailViewModel : OrderBaseViewModel
    {
        public OrderDetailViewModel()
        {
        }

        public OrderDetailViewModel(Order order) : base(order)
        {
            if (order != null)
            {
                UserId = order.UserId;
                CustomerName = order.User.Name;
                CustomerPhone = order.User.PhoneNumber;
                if (order.Station != null)
                    Station = new StationBaseViewModel(order.Station);
                if (order.OrderDetails.Any())
                    Services = order.OrderDetails.Select(x => new ServiceViewModel(x)).ToList();
            }
        }

        public string UserId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public List<ServiceViewModel> Services { get; set; }

        public StationBaseViewModel Station { get; set; }
    }
}
