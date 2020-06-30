using System.ComponentModel.DataAnnotations;

namespace VehicleRepairs.Api.Services.Identity.Models
{
    public class UserUpdateRequestModel
    {
        public string PhoneNumber { get; set; }

        [StringLength(255, ErrorMessage = "Tên không hợp lệ", MinimumLength = 2)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }
    }
}
