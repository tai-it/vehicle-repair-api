namespace VehicleRepairs.Database.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public User()
        {
            CreatedOn = DateTime.Now;
            IsActive = true;
            UserRoles = new List<UserRole>();
            Stations = new List<Station>();
            Orders = new List<Order>();
            Notifications = new List<Notification>();
        }

        public string SecurityNumber { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<Station> Stations { get; set; }

        public List<Order> Orders { get; set; }

        public List<Notification> Notifications { get; set; }
    }
}
