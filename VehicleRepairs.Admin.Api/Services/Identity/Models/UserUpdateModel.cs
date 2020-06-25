namespace VehicleRepairs.Admin.Api.Services.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserUpdateModel
    {
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }
    }
}
