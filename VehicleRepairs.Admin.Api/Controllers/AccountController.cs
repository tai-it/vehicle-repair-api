namespace VehicleRepairs.Admin.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VehicleRepairs.Admin.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Admin.Api.Infrastructure.Filters;
    using VehicleRepairs.Admin.Api.Services.Identity;
    using VehicleRepairs.Admin.Api.Services.Identity.Models;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService<User> _identityService;

        public AccountController(IIdentityService<User> identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("users")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<PagedList<UserBaseViewModel>> GetUsers([FromQuery] BaseRequestModel request)
        {
            var users = await _identityService.GetUsersAsync(request);
            return users;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var responseModel = await _identityService.LoginAsync(model);
            return new CustomActionResult(responseModel);
        }

        [HttpPost("register")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var responseModel = await _identityService.RegisterAsync(model);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Disabse(string id, [FromBody] DisableUserRequest request)
        {
            request.UserId = id;
            var responseModel = await _identityService.DisableUserAsync(request);
            return new CustomActionResult(responseModel);
        }
    }
}
