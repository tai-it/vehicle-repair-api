namespace VehicleRepairs.Api.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Api.Infrastructure.Common;

    [Table("Notifications")]
    public class Notification : BaseEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsSeen { get; set; }

        public string[] Targets { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
