namespace VehicleRepairs.Api.Services.Identity.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Api.Domain.Entities;

    public class UserBaseViewModel
    {
        public UserBaseViewModel()
        {
        }

        public UserBaseViewModel(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Name = user.Name;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                Roles = user.UserRoles.Select(x => x.Role.Name).ToList();
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Roles { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
