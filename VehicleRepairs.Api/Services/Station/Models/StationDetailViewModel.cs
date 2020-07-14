namespace VehicleRepairs.Api.Services.Station.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using VehicleRepairs.Api.Services.Service.Models;
    using VehicleRepairs.Database.Domain.Entities;

    public class StationDetailViewModel : StationBaseViewModel
    {
        public StationDetailViewModel() : base() 
        {
            Services = new List<ServiceViewModel>();
        }

        public StationDetailViewModel(Station station) : base(station)
        {
            Services = station.Services
                .Where(x => !x.IsDeleted)
                    .Select(x => new ServiceViewModel(x)).ToList();
            Owner = new StationOwner(station.User);
        }

        public StationOwner Owner { get; set; }

        public List<ServiceViewModel> Services { get; set; }
    }
}
