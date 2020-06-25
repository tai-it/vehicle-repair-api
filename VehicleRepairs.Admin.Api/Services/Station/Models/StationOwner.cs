namespace VehicleRepairs.Admin.Api.Services.Station.Models
{
    using VehicleRepairs.Database.Domain.Entities;

    public class StationOwner
    {
        public StationOwner()
        {
        }

        public StationOwner(User user)
        {
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
        }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
