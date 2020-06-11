namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Domain.Contexts;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Infrastructure.Utilities;
    using VehicleRepairs.Api.Services.Station.Models;

    public class StationPagedListHandler : IRequestHandler<StationPagedListRequest, PagedList<StationBaseViewModel>>
    {
        private readonly ApplicationDbContext db;

        public StationPagedListHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<StationBaseViewModel>> Handle(StationPagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Stations
                .Where(x => !x.IsDeleted)
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                        .Select(x => new StationBaseViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var viewModelType = typeof(StationBaseViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<StationBaseViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var stationBaseViewModel = new StationBaseViewModel();
            var type = stationBaseViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
