namespace VehicleRepairs.Database.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Shared.Common;

    [Table("Notifications")]
    public class Notification : BaseEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string Type { get; set; }

        public bool IsSeen { get; set; }

        public bool IsSent { get; set; }

        public string Target { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
