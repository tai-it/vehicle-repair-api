namespace VehicleRepairs.Admin.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Admin.Api.Infrastructure.Filters;
    using VehicleRepairs.Admin.Api.Services.Station;
    using VehicleRepairs.Admin.Api.Services.Station.Models;
    using VehicleRepairs.Shared.Common;

    [Route("api/stations")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
    public class StationController : ControllerBase
    {
        private readonly IMediator mediator;

        public StationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedList<StationDetailViewModel>> GetAsync([FromQuery] StationPagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new StationGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new StationDeleteRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
