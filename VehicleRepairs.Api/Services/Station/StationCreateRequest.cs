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
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tên là trường bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Địa chỉ là trường bắt buộc")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Kinh độ là trường bắt buộc")]
        public decimal Latitude { get; set; }

        [Required(ErrorMessage = "Vĩ độ là trường bắt buộc")]
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Phương tiện là trường bắt buộc")]
        public string Vehicle { get; set; }

        public bool IsAvailable { get; set; } = false;

        public bool HasAmbulatory { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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
