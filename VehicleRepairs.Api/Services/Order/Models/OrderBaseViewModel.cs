namespace VehicleRepairs.Api.Services.Order.Models
{
    using System;
    using VehicleRepairs.Api.Domain.Entities;

    public class OrderBaseViewModel
    {
        public OrderBaseViewModel() { }

        public OrderBaseViewModel(Order order)
        {
            if (order != null)
            {
                UserId = order.UserId;
                StationId = order.StationId;
                Address = order.Address;
                Latitude = order.Latitude;
                Longitude = order.Longitude;
                Distance = order.Distance;
                UseAmbulatory = order.UseAmbulatory;
                AmbulatoryFee = order.AmbulatoryFee;
                Status = order.Status;
                TotalPrice = order.TotalPrice;
            }
        }

        public string UserId { get; set; }

        public Guid StationId { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Distance { get; set; }

        public bool UseAmbulatory { get; set; }

        public decimal AmbulatoryFee { get; set; }

        public decimal? TotalPrice { get; set; }

        public string Status { get; set; }
    }
}
