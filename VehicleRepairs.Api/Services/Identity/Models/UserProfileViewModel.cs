namespace VehicleRepairs.Api.Services.Identity.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class UserProfileViewModel
    {
        public UserProfileViewModel(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Name = user.Name;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                DeviceToken = user.DeviceToken;
                Orders = user.Orders.Select(x => new OrderBaseViewModel(x)).ToList();
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }

        public List<OrderBaseViewModel> Orders { get; set; }
    }
}
