namespace VehicleRepairs.Admin.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Identity.Models;
    using VehicleRepairs.Admin.Api.Services.Ordering.Models;
    using VehicleRepairs.Admin.Api.Services.Statistics.OrderStatistics;
    using VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics;
    using VehicleRepairs.Admin.Api.Services.Statistics.StationStatistics.Models;
    using VehicleRepairs.Admin.Api.Services.Statistics.UserSatistics;
    using VehicleRepairs.Shared.Common;

    [Route("api/dashboard")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator mediator;

        public DashboardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("users")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<PagedList<UserBaseViewModel>> GetUsers([FromQuery] GetUsersRequest request)
        {
            return await this.mediator.Send(request);
        }

        [HttpGet("orders")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<PagedList<OrderDetailViewModel>> GetOrders([FromQuery] GetOrdersRequest request)
        {
            return await this.mediator.Send(request);
        }

        [HttpGet("stations")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<PagedList<StationStatisticViewModel>> GetStations([FromQuery] GetStationsStatisticRequest request)
        {
            return await this.mediator.Send(request);
        }
    }
}
