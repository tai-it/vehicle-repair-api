namespace VehicleRepairs.Admin.Api.Services.Station.Models
{
    using System;
    using VehicleRepairs.Database.Domain.Entities;

    public class StationBaseViewModel
    {
        public StationBaseViewModel() { }

        public StationBaseViewModel(Station station)
        {
            if (station != null)
            {
                Id = station.Id;
                Name = station.Name;
                Address = station.Address;
                Latitude = station.Latitude;
                Longitude = station.Longitude;
                Vehicle = station.Vehicle;
                IsAvailable = station.IsAvailable;
                HasAmbulatory = station.HasAmbulatory;
                Coefficient = station.Coefficient;
                CreatedOn = station.CreatedOn;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Vehicle { get; set; }

        public bool IsAvailable { get; set; }

        public bool HasAmbulatory { get; set; }

        public decimal Coefficient { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
