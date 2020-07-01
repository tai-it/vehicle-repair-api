namespace VehicleRepairs.Api.Services.Station
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Infrastructure.Utilities;
    using VehicleRepairs.Api.Services.Station.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Caching;
    using VehicleRepairs.Shared.Common;

    public class StationPagedListHandler : IRequestHandler<StationPagedListRequest, PagedList<StationDetailViewModel>>
    {
        private readonly ApplicationDbContext db;

        private readonly ICacheManager _cacheManager;

        public StationPagedListHandler(ApplicationDbContext db, ICacheManager cacheManager)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            _cacheManager = cacheManager;
        }

        public async Task<PagedList<StationDetailViewModel>> Handle(StationPagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await _cacheManager.GetAndSetAsync("list_stations", 60, async () =>
            {
                return await this.db.Stations
                    .Where(x => !x.IsDeleted)
                        .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                        .Where(x => (string.IsNullOrEmpty(request.Vehicle)) || (x.Vehicle.ToLower().Equals(request.Vehicle.ToLower())))
                        .Where(x => (!request.HasAmbulatory) || (x.HasAmbulatory))
                        .Where(x => (string.IsNullOrEmpty(request.ServiceName)) || (x.Services.FirstOrDefault(x => x.Name.ToLower().Contains(request.ServiceName.ToLower()))) != null)
                            .Include(x => x.User)
                            .Include(x => x.Services)
                                .Select(x => new StationDetailViewModel(x)).ToListAsync();
            });

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var viewModelType = typeof(StationDetailViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<StationDetailViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var viewModel = new StationDetailViewModel();
            var type = viewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
