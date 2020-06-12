namespace VehicleRepairs.Api.Services.Ordering.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderDetailCreateModel
    {
        [Required]
        public Guid ServiceId { get; set; }
    }
}
