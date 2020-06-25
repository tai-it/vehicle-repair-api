namespace VehicleRepairs.Api.Services.Ordering
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Infrastructure.Utilities;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class GetStationOrdersHandler : IRequestHandler<GetStationOrdersRequest, PagedList<OrderDetailViewModel>>
    {
        private readonly ApplicationDbContext db;

        public GetStationOrdersHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<OrderDetailViewModel>> Handle(GetStationOrdersRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Orders
                .Where(x => !x.IsDeleted && x.StationId == request.Id)
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Address.Contains(request.Query)))
                        .Include(x => x.OrderDetails)
                            .ThenInclude(x => x.Service)
                        .Include(x => x.User)
                        .Include(x => x.Station)
                            .Select(x => new OrderDetailViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Status";
            }

            var viewModelType = typeof(OrderDetailViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<OrderDetailViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var stationDetailViewModel = new OrderDetailViewModel();
            var type = stationDetailViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
