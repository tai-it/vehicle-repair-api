namespace VehicleRepairs.Api.Services.Service
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class ServiceCreateRequest : IValidatableObject, IRequest<ResponseModel>
    {
        public Guid StationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var station = db.Stations.FirstOrDefault(x => x.Id == StationId && !x.IsDeleted);

            if (station == null)
            {
                yield return new ValidationResult("Station id not found", new string[] { "StationId" });
            }

            var service = db.Services.FirstOrDefault(x => !x.IsDeleted && x.Id == StationId && x.Name == Name);

            if (service != null)
            {
                yield return new ValidationResult("Station name has already existed", new string[] { "Name" });
            }
        }
    }
}
