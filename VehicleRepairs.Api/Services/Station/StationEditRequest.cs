namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;

    public class StationEditRequest : IValidatableObject, IRequest<ResponseModel>
    {
        [Phone]
        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Vehicle { get; set; }

        public bool IsAvailable { get; set; }

        public bool HasAmbulatory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name != null)
            {
                var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

                var station = db.Stations.FirstOrDefault(x => x.Name == Name && x.Address == Address);

                if (station != null)
                {
                    yield return new ValidationResult("Cửa hàng này đã tồn tại", new string[] { "Name" });
                }
            }
        }
    }
}
