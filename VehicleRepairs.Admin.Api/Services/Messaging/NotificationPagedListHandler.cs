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

    public class NotificationPagedListHandler : IRequestHandler<NotificationPagedListRequest, PagedList<NotificationBaseViewModel>>
    {
        private readonly ApplicationDbContext db;

        public NotificationPagedListHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<NotificationBaseViewModel>> Handle(NotificationPagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Notifications
                        .Include(x => x.User)
                        .Select(x => new NotificationBaseViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "CreatedOn";
            }

            var viewModelType = typeof(NotificationBaseViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<NotificationBaseViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var notificationBaseViewModel = new NotificationBaseViewModel();
            var type = notificationBaseViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
