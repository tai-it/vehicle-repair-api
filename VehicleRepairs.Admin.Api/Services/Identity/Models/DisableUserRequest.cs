namespace VehicleRepairs.Admin.Api.Services.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DisableUserRequest
    {
        public string UserId { get; set; }

        public bool IsActive { get; set; }
    }
}
