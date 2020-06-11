namespace VehicleRepairs.Api.Services.Order.Models
{
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
            Services = order.OrderDetails.Select(x => new ServiceViewModel(x)).ToList();
        }

        public List<ServiceViewModel> Services { get; set; }
    }
}
