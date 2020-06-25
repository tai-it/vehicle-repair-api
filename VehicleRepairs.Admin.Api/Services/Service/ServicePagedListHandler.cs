namespace VehicleRepairs.Admin.Api.Services.Service
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Infrastructure.Utilities;
    using VehicleRepairs.Admin.Api.Services.Service.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class ServicePagedListHandler : IRequestHandler<ServicePagedListRequest, PagedList<ServiceViewModel>>
    {
        private readonly ApplicationDbContext db;

        public ServicePagedListHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<ServiceViewModel>> Handle(ServicePagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Services
                .Where(x => !x.IsDeleted)
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                    .Where(x => (string.IsNullOrEmpty(request.Vehicle)) || (x.Station.Vehicle.ToLower().Equals(request.Vehicle.ToLower())))
                        .Select(x => new ServiceViewModel(x)).ToListAsync();

            if (request.IsDistinct)
            {
                list = list.GroupBy(x => x.Name).Select(g => g.First()).ToList();
            }

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var viewModelType = typeof(ServiceViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<ServiceViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var serviceViewModel = new ServiceViewModel();
            var type = serviceViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
