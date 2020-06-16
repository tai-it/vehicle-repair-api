namespace VehicleRepairs.Api.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
