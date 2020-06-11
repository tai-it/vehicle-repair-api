namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationCreateRequest : IValidatableObject, IRequest<ResponseModel>
    {
        [Required]
        public string UserId { get; set; }

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

        public bool IsAvailable { get; set; } = false;

        public bool HasAmbulatory { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var station = db.Stations.FirstOrDefault(x => x.Name == Name && x.Address == Address);

            if (station != null)
            {
                yield return new ValidationResult("This station has already existed", new string[] { "Name" });
            }
        }
    }
}
