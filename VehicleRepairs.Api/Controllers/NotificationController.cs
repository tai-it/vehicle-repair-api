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
    using VehicleRepairs.Api.Services.Messaging;
    using VehicleRepairs.Api.Services.Messaging.Models;

    [Route("api/notifications")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator mediator;

        public NotificationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<PagedList<NotificationDetailViewModel>> GetAsync([FromQuery] NotificationPagedListRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> MarkAllAsReadAsync(CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = new NotificationMarkAllAsReadRequest() { PhoneNumber = phoneNumber };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = new NotificationGetByIdRequest() 
            { 
                Id = id,
                PhoneNumber = phoneNumber
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
