namespace VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics.Models
{
    using System;
    using System.Linq;
    using VehicleRepairs.Shared.Common;

    public class StationStatisticViewModel
    {

        public StationStatisticViewModel()
        {
            TotalRevenue = 0;
        }

        public StationStatisticViewModel(Database.Domain.Entities.Station station) : base()
        {
            if (station != null)
            {
                var completedOrders = station.Orders.Where(x => x.Status == CommonConstants.OrderStatus.DONE).ToList();
                foreach (var order in completedOrders)
                {
                    TotalRevenue += order.TotalPrice;
                }
                Id = station.Id;
                StationName = station.Name;
                StationOwner = station.User.Name;
                TotalOrder = completedOrders.Count;
                TotalService = station.Services.Count;
            }
        }

        public Guid Id { get; set; }

        public string StationName { get; set; }

        public string StationOwner { get; set; }

        public int TotalOrder { get; set; }

        public int TotalService { get; set; }

        public decimal? TotalRevenue { get; set; } = 0;
    }
}
