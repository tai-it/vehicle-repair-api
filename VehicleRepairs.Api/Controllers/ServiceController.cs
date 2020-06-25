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
    using VehicleRepairs.Api.Services.Service;
    using VehicleRepairs.Api.Services.Service.Models;
    using VehicleRepairs.Shared.Common;

    [Route("api/services")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedList<ServiceViewModel>> GetAsync([FromQuery] ServicePagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new ServiceGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("vehicle={vehicle}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistinctServiceByVehicleAsync(string vehicle, CancellationToken cancellationToken)
        {
            var request = new GetDistinctServiceByVehicleRequest() { Vehicle = vehicle };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> PostAsync([FromBody] ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ServiceEditRequest request, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            request.Id = id;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = CommonConstants.Roles.STATION)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = new ServiceDeleteRequest() 
            { 
                Id = id,
                PhoneNumber = phoneNumber
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
