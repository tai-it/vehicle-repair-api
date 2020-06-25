namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Messaging.Models;
    using VehicleRepairs.Admin.Api.Infrastructure.Utilities;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class NotificationPagedListHandler : IRequestHandler<NotificationPagedListRequest, PagedList<NotificationDetailViewModel>>
    {
        private readonly ApplicationDbContext db;

        public NotificationPagedListHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<NotificationDetailViewModel>> Handle(NotificationPagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Notifications
                .Where(x => !x.IsDeleted)
                    .Where(x => x.User.PhoneNumber == request.PhoneNumber)
                        .Include(x => x.User)
                        .Include(x => x.Order)
                            .ThenInclude(x => x.Station)
                        .Include(x => x.Order)
                            .ThenInclude(x => x.OrderDetails)
                                .ThenInclude(x => x.Service)
                        .Select(x => new NotificationDetailViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "CreatedOn";
            }

            var viewModelType = typeof(NotificationDetailViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<NotificationDetailViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var notificationDetailViewModel = new NotificationDetailViewModel();
            var type = notificationDetailViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
