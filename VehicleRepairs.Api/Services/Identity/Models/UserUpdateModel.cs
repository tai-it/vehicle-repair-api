using System.ComponentModel.DataAnnotations;

namespace VehicleRepairs.Api.Services.Identity.Models
{
    public class UserUpdateModel
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }
    }
}
