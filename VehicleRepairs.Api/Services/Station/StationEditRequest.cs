namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

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

        public decimal Coefficient { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Vehicle != CommonConstants.Vehicles.MOTOBIKE && Vehicle != CommonConstants.Vehicles.CAR)
            {
                yield return new ValidationResult("Không tìm thấy phương tiện này", new string[] { "Vehicle" });
            }

            if (!string.IsNullOrEmpty(Address))
            {
                if (Latitude == decimal.Zero)
                {
                    yield return new ValidationResult("Kinh độ là trường bắt buộc", new string[] { "Latitude" });
                }

                if (Latitude == decimal.Zero)
                {
                    yield return new ValidationResult("Vĩ độ là trường bắt buộc", new string[] { "Longitude" });
                }
            }

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
