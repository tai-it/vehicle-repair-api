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
    using VehicleRepairs.Api.Infrastructure.Filters;
    using VehicleRepairs.Api.Services.Ordering;
    using VehicleRepairs.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    [Route("api/orders")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<PagedList<OrderDetailViewModel>> GetMineOdersAsync([FromQuery] GetMineOrderRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.USER)]
        public async Task<IActionResult> PostAsync([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new OrderGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("stations/{stationId}")]
        [Authorize(Roles = CommonConstants.Roles.STATION + "," + CommonConstants.Roles.SUPER_ADMIN + "," + CommonConstants.Roles.ADMIN)]
        public async Task<PagedList<OrderDetailViewModel>> GetAsync(Guid stationId, [FromQuery] GetStationOrdersRequest request, CancellationToken cancellationToken)
        {
            request.Id = stationId;
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.USER + "," + CommonConstants.Roles.STATION)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] OrderEditRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            request.Id = id;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
