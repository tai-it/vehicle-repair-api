namespace VehicleRepairs.Api.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using VehicleRepairs.Api.Infrastructure.Common;

    [Table("Stations")]
    public class Station : BaseEntity
    {
        public Station() : base()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public string Vehicle { get; set; }

        public bool IsAvailable { get; set; }

        public bool HasAmbulatory { get; set; }

        public int? StarRating
        {
            get
            {
                if (TotalRating != 0)
                {
                    int totalStar = 0;

                    foreach (var review in Reviews)
                    {
                        totalStar += review.StarRating;
                    }

                    return (int)totalStar / this.TotalRating;
                }
                return 0;
            }
        }

        public int? TotalRating
        {
            get
            {
                if (Reviews.Any())
                {
                    return Reviews.Count;
                }
                return 0;
            }
        }

        public List<Review> Reviews { get; set; }

        public List<Service> Services { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
