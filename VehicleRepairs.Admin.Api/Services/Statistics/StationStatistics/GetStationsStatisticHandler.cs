namespace VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Infrastructure.Utilities;
    using VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class GetStationsStatisticHandler : IRequestHandler<GetStationsStatisticRequest, PagedList<StationStatisticViewModel>>
    {
        private readonly ApplicationDbContext db;

        public GetStationsStatisticHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<StationStatisticViewModel>> Handle(GetStationsStatisticRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Stations
                .Where(x => (x.CreatedOn.Value.Date >= request.FromDate.Date && x.CreatedOn.Value.Date < request.ToDate.Date))
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                        .Include(x => x.Orders)
                            .ThenInclude(x => x.OrderDetails)
                                .ThenInclude(x => x.Service)
                        .Include(x => x.User)
                        .Include(x => x.Services)
                            .Select(x => new StationStatisticViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "CreatedOn";
            }

            var viewModelType = typeof(StationStatisticViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<StationStatisticViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var viewModel = new StationStatisticViewModel();
            var type = viewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
