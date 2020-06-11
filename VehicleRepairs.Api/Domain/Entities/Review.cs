namespace VehicleRepairs.Api.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Api.Infrastructure.Common;

    [Table("Reviews")]
    public class Review : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public Guid StationId { get; set; }

        public Station Station { get; set; }

        public int StarRating { get; set; }

        public string Message { get; set; }
    }
}
