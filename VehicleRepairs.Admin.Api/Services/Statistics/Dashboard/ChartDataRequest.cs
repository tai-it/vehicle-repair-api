namespace VehicleRepairs.Admin.Api.Services.Statistics.Dashboard
{
    using MediatR;
    using System;
    using VehicleRepairs.Shared.Common;

    public class ChartDataRequest : IRequest<ResponseModel>
    {

        public ChartDataRequest()
        {
            this.ChartType = CommonConstants.ChartTypes.RANGE;
            this.FromDate = DateTime.Now.AddDays(-6);
            this.ToDate = DateTime.Now;
        }
        
        public string ChartType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
