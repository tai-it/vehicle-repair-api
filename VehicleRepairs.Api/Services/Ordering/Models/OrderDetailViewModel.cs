namespace VehicleRepairs.Api.Services.Ordering.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Services.Service.Models;

    public class OrderDetailViewModel : OrderBaseViewModel
    {
        public OrderDetailViewModel()
        {
        }

        public OrderDetailViewModel(Order order) : base(order)
        {
            UserId = order.UserId;
            CustomerName = order.User.Name;
            CustomerPhone = order.User.PhoneNumber;
            StationId = order.StationId;
            Services = order.OrderDetails.Select(x => new ServiceViewModel(x)).ToList();
        }

        public string UserId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public Guid StationId { get; set; }

        public List<ServiceViewModel> Services { get; set; }
    }
}
