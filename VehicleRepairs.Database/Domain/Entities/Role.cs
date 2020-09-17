namespace VehicleRepairs.Database.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        public List<UserRole> UserRoles { get; set; }
    }
}
