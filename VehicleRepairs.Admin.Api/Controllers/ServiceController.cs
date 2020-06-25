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
    using VehicleRepairs.Admin.Api.Services.Service;
    using VehicleRepairs.Admin.Api.Services.Service.Models;
    using VehicleRepairs.Shared.Common;

    [Route("api/services")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedList<ServiceViewModel>> Get([FromQuery] ServicePagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var request = new ServiceGetByIdRequest() { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
