namespace VehicleRepairs.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Api.Infrastructure.Common;
    using VehicleRepairs.Api.Infrastructure.Filters;
    using VehicleRepairs.Api.Services.Order;

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

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.USER)]
        public async Task<IActionResult> PostAync([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
