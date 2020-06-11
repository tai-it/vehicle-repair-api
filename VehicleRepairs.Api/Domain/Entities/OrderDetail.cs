namespace VehicleRepairs.Api.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Api.Infrastructure.Common;
    
    [Table("OrderDetails")]
    public class OrderDetail : BaseEntity
    {
        [Required]
        public Guid ServiceId { get; set; }

        public Service Service { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}