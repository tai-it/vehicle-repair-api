namespace VehicleRepairs.Api.Services.Order.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderDetailCreateModel
    {
        [Required]
        public Guid ServiceId { get; set; }
    }
}
