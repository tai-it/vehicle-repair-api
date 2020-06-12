namespace VehicleRepairs.Api.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public string SecurityNumber { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }

        public Station Station { get; set; }

        public List<Order> Orders { get; set; }
    }
}
