﻿namespace VehicleRepairs.Api.Services.Ordering
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
    using VehicleRepairs.Api.Services.Ordering.Models;

    public class GetStationOrdersHandler : IRequestHandler<GetStationOrdersRequest, PagedList<OrderBaseViewModel>>
    {
        private readonly ApplicationDbContext db;

        public GetStationOrdersHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<OrderBaseViewModel>> Handle(GetStationOrdersRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Orders
                .Where(x => !x.IsDeleted && x.StationId == request.Id)
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Address.Contains(request.Query)))
                        .Include(x => x.OrderDetails)
                            .ThenInclude(x => x.Service)
                        .Include(x => x.User)
                            .Select(x => new OrderBaseViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Status";
            }

            var viewModelType = typeof(OrderBaseViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<OrderBaseViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var stationBaseViewModel = new OrderBaseViewModel();
            var type = stationBaseViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
