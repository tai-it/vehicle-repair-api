namespace VehicleRepairs.Api.Services.Ordering.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Api.Services.Service.Models;
    using VehicleRepairs.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Entities;

    public class OrderDetailViewModel : OrderBaseViewModel
    {
        public OrderDetailViewModel()
        {
            Services = new List<ServiceViewModel>();
        }

        public OrderDetailViewModel(Order order) : base(order)
        {
            UserId = order.UserId;
            CustomerName = order.User.Name;
            CustomerPhone = order.User.PhoneNumber;
            Station = new StationBaseViewModel(order.Station);
            Services = order.OrderDetails.Select(x => new ServiceViewModel(x)).ToList();
        }

        public string UserId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public List<ServiceViewModel> Services { get; set; }

        public StationBaseViewModel Station { get; set; }
    }
}
