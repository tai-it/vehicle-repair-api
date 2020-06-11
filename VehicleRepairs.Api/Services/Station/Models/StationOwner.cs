using VehicleRepairs.Api.Domain.Entities;

namespace VehicleRepairs.Api.Services.Station.Models
{
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
