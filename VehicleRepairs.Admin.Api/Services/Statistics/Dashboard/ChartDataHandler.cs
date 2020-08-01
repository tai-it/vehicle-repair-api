namespace VehicleRepairs.Admin.Api.Services.Statistics.Dashboard
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Statistics.Dashboard.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class ChartDataHandler : IRequestHandler<ChartDataRequest, ResponseModel>
    {
        private readonly ApplicationDbContext db;

        public ChartDataHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(ChartDataRequest request, CancellationToken cancellationToken)
        {
            var chartData = new ChartDataResponseModel();

            switch (request.ChartType.ToUpper())
            {
                case CommonConstants.ChartTypes.YEAR:
                    chartData = await this.GetByYear(request.FromDate.Year);
                    break;

                case CommonConstants.ChartTypes.MONTH:
                    chartData = await this.GetByMonth(request.FromDate.Year, request.FromDate.Month);
                    break;

                default:
                    chartData = await this.GetByRange(request.FromDate, request.ToDate);
                    break;
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = chartData
            };
        }

        private async Task<ChartDataResponseModel> GetByRange(DateTime fromDate, DateTime toDate)
        {
            var dates = this.GetDates(fromDate, toDate);

            var users = await this.db.Users
                .Where(x => (x.CreatedOn.Value.Date >= fromDate.Date && x.CreatedOn.Value.Date <= toDate.Date))
                .ToListAsync();

            var userData = dates
                .Where(date => date.Date <= DateTime.Now.Date)
                .Select(date => users.Where(x => x.CreatedOn.Value.Date == date.Date).ToList().Count).ToList();

            var stations = await this.db.Stations
                .Where(x => !x.IsDeleted)
                .Where(x => (x.CreatedOn.Value.Date >= fromDate.Date && x.CreatedOn.Value.Date <= toDate.Date))
                .ToListAsync();

            var stationData = dates
                .Where(date => date.Date <= DateTime.Now.Date)
                .Select(date => stations.Where(x => x.CreatedOn.Value.Date == date.Date).ToList().Count).ToList();

            var orders = await this.db.Orders
                .Where(x => x.Status.Equals(CommonConstants.OrderStatus.DONE))
                .Where(x => (x.CreatedOn.Value.Date >= fromDate.Date && x.CreatedOn.Value.Date <= toDate.Date))
                .ToListAsync();

            var orderData = dates
                .Where(date => date.Date <= DateTime.Now.Date)
                .Select(date => orders.Where(x => x.CreatedOn.Value.Date == date.Date).ToList().Count).ToList();

            return new ChartDataResponseModel()
            {
                ChartType = CommonConstants.ChartTypes.RANGE,
                Labels = this.GetDates(fromDate, toDate).Select(x => x.ToString("dd MMM")).ToList(),
                FromDate = fromDate.ToString("dd MMM yyyy"),
                ToDate = toDate.ToString("dd MMM yyyy"),
                UserData = userData,
                StationData = stationData,
                OrderData = orderData
            };
        }

        private async Task<ChartDataResponseModel> GetByMonth(int year, int month)
        {
            var lastDateOfMonth = DateTime.DaysInMonth(year, month);
            var fromDate = new DateTime(year, month, 1).Date;
            var toDate = new DateTime(year, month, lastDateOfMonth).Date;

            var users = await this.db.Users
                .Where(x => (x.CreatedOn.Value.Date >= fromDate && x.CreatedOn.Value.Date <= toDate))
                .ToListAsync();

            var userData = Enumerable.Range(1, lastDateOfMonth)
                .Where(day => new DateTime(year, month, day).Date <= DateTime.Now.Date)
                .Select(day => users.Where(x => x.CreatedOn.Value.Day == day).ToList().Count).ToList();

            var stations = await this.db.Stations
                .Where(x => !x.IsDeleted)
                .Where(x => (x.CreatedOn.Value.Date >= fromDate && x.CreatedOn.Value.Date <= toDate))
                .ToListAsync();

            var stationData = Enumerable.Range(1, lastDateOfMonth)
                .Where(day => new DateTime(year, month, day).Date <= DateTime.Now.Date)
                .Select(day => stations.Where(x => x.CreatedOn.Value.Day == day).ToList().Count).ToList();

            var orders = await this.db.Orders
                .Where(x => x.Status.Equals(CommonConstants.OrderStatus.DONE))
                .Where(x => (x.CreatedOn.Value.Date >= fromDate && x.CreatedOn.Value.Date <= toDate))
                .ToListAsync();

            var orderData = Enumerable.Range(1, lastDateOfMonth)
                .Where(day => new DateTime(year, month, day).Date <= DateTime.Now.Date)
                .Select(day => orders.Where(x => x.CreatedOn.Value.Day == day).ToList().Count).ToList();

            return new ChartDataResponseModel()
            {
                ChartType = CommonConstants.ChartTypes.MONTH,
                Labels = this.GetDates(fromDate, toDate).Select(x => x.ToString("dd")).ToList(),
                FromDate = fromDate.ToString("dd MMM yyyy"),
                ToDate = toDate.ToString("dd MMM yyyy"),
                UserData = userData,
                StationData = stationData,
                OrderData = orderData
            };
        }

        private async Task<ChartDataResponseModel> GetByYear(int year)
        {
            var users = await this.db.Users
                .Where(x => (x.CreatedOn.Value.Date.Year == year))
                .ToListAsync();

            var userData = Enumerable.Range(1, 12)
                .Where(month => new DateTime(year, month, 1) <= DateTime.Now)
                .Select(month => users.Where(x => x.CreatedOn.Value.Month == month).ToList().Count).ToList();

            var stations = await this.db.Stations
                .Where(x => !x.IsDeleted)
                .Where(x => (x.CreatedOn.Value.Date.Year == year))
                .ToListAsync();

            var stationData = Enumerable.Range(1, 12)
                .Where(month => new DateTime(year, month, 1) <= DateTime.Now)
                .Select(month => stations.Where(x => x.CreatedOn.Value.Month == month).ToList().Count).ToList();

            var orders = await this.db.Orders
                .Where(x => x.Status.Equals(CommonConstants.OrderStatus.DONE))
                .Where(x => (x.CreatedOn.Value.Date.Year == year))
                .ToListAsync();

            var orderData = Enumerable.Range(1, 12)
                .Where(month => new DateTime(year, month, 1) <= DateTime.Now)
                .Select(month => orders.Where(x => x.CreatedOn.Value.Month == month).ToList().Count).ToList();

            return new ChartDataResponseModel()
            {
                ChartType = CommonConstants.ChartTypes.YEAR,
                Labels = DateTimeFormatInfo.CurrentInfo.MonthNames.Where(x => !string.IsNullOrEmpty(x)).ToList(),
                FromDate = new DateTime(year, 1, 1).ToString("dd MMM yyyy"),
                ToDate = new DateTime(year, 12, 31).ToString("dd MMM yyyy"),
                UserData = userData,
                StationData = stationData,
                OrderData = orderData
            };
        }

        private List<DateTime> GetDates(DateTime fromDate, DateTime toDate)
        {
            return Enumerable
                    .Range(0, int.MaxValue)
                    .Select(index => fromDate.AddDays(index))
                    .TakeWhile(date => date <= toDate)
                    .ToList();
        }
    }
}
