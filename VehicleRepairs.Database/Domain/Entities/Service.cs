namespace VehicleRepairs.Database.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VehicleRepairs.Shared.Common;

    [Table("Services")]
    public class Service : BaseEntity
    {
        public Service() : base()
        {

        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Guid StationId { get; set; }

        public Station Station { get; set; }
    }
}
