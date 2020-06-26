namespace VehicleRepairs.Admin.Api.Services.Statistics.UserSatistics
{
    using MediatR;
    using System;
    using VehicleRepairs.Admin.Api.Services.Identity.Models;
    using VehicleRepairs.Shared.Common;

    public class GetUsersRequest : BaseRequestModel, IRequest<PagedList<UserBaseViewModel>>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
