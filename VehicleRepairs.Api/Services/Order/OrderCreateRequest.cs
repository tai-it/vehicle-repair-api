namespace VehicleRepairs.Api.Services.Order
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Services.Order.Models;

    public class OrderCreateRequest : IRequest<ResponseModel>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid StationId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public bool UseAmbulatory { get; set; }

        public List<OrderDetailCreateModel> OrderDetails { get; set; }
    }
}
