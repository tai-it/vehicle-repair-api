namespace VehicleRepairs.Api.Services.Service.Models
{
    using System;
    using VehicleRepairs.Database.Domain.Entities;

    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
        }

        public ServiceViewModel(Service service)
        {
            if (service != null)
            {
                Id = service.Id;
                Name = service.Name;
                Description = service.Description;
                Thumbnail = service.Thumbnail;
                Price = service.Price;
                StationId = service.StationId;
            }
        }

        public ServiceViewModel(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                Id = orderDetail.Service.Id;
                Name = orderDetail.Service.Name;
                Description = orderDetail.Service.Description;
                Thumbnail = orderDetail.Service.Thumbnail;
                Price = orderDetail.Service.Price;
                StationId = orderDetail.Service.StationId;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }

        public Guid StationId { get; set; }
    }
}
