namespace VehicleRepairs.Database.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Shared.Common;

    [Table("Notifications")]
    public class Notification : BaseEntity
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Data { get; set; }

        public bool IsSeen { get; set; }

        public bool IsSent { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
