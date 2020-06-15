namespace VehicleRepairs.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Infrastructure.Filters;
    using VehicleRepairs.Api.Services.Station;
    using VehicleRepairs.Api.Services.Station.Models;

    [Route("api/stations")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class StationController : ControllerBase
    {
        private readonly IMediator mediator;

        public StationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedList<StationBaseViewModel>> GetAsync([FromQuery] StationPagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new StationGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> PostAsync([FromBody] StationCreateRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] StationEditRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.Id = id;
            request.PhoneNumber = phoneNumber;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = new StationDeleteRequest()
            {
                Id = id,
                PhoneNumber = phoneNumber
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
