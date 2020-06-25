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
    using VehicleRepairs.Admin.Api.Services.Ordering;
    using VehicleRepairs.Admin.Api.Services.Ordering.Models;
    using VehicleRepairs.Shared.Common;

    [Route("api/orders")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new OrderGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("stations/{id}")]
        public async Task<PagedList<OrderDetailViewModel>> GetAsync(Guid id, [FromQuery] GetStationOrdersRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await this.mediator.Send(request, cancellationToken);
        }
    }
}
