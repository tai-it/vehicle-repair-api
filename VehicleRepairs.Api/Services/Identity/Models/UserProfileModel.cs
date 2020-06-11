namespace VehicleRepairs.Api.Services.Identity.Models
{
    using VehicleRepairs.Api.Domain.Entities;

    public class UserProfileModel
    {
        public UserProfileModel(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Name = user.Name;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                DeviceToken = user.DeviceToken;
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }
    }
}
