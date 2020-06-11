namespace VehicleRepairs.Api.Infrastructure.Common
{
    public class BaseRequestModel
    {
        public BaseRequestModel()
        {
            IsDesc = false;
        }

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public string SortName { get; set; }

        public bool IsDesc { get; set; }

        public string Query { get; set; }

        public bool? IsActive { get; set; }
    }
}
