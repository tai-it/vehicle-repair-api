namespace VehicleRepairs.Admin.Api.Services.Statistics.UserSatistics
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Infrastructure.Utilities;
    using VehicleRepairs.Admin.Api.Services.Identity.Models;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class GetUsersHandler : IRequestHandler<GetUsersRequest, PagedList<UserBaseViewModel>>
    {
        private readonly ApplicationDbContext db;

        public GetUsersHandler(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PagedList<UserBaseViewModel>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await this.db.Users
                .Where(x => (x.CreatedOn >= request.FromDate && x.CreatedOn < request.ToDate))
                .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role)
                    .Select(x => new UserBaseViewModel(x))
                        .ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var viewModelType = typeof(UserBaseViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            users = request.IsDesc ? users.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : users.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<UserBaseViewModel>(users, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var viewModel = new UserBaseViewModel();
            var type = viewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
