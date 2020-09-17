using System.Collections.Generic;
using System.Linq;

namespace VehicleRepairs.Admin.Api.Services.Statistics.Dashboard.Models
{
    public class ChartDataResponseModel
    {
        public string ChartType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public List<string> Labels { get; set; }

        public int ChartHeight
        {
            get
            {
                var maxValue = UserData.Union(StationData.Union(OrderData)).Max();
                return (maxValue + 20) - (maxValue % 10);
            }
        }

        public List<int> UserData { get; set; }

        public List<int> StationData { get; set; }

        public List<int> OrderData { get; set; }
    }
}
