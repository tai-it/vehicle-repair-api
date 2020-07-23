namespace VehicleRepairs.Admin.Api.Controllers
{
    using System.Security.Claims;
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
    [Authorize(Roles = CommonConstants.Roles.ADMIN + "," + CommonConstants.Roles.SUPER_ADMIN)]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService<User> _identityService;

        public AccountController(IIdentityService<User> identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("users")]
        public async Task<PagedList<UserBaseViewModel>> GetUsers([FromQuery] BaseRequestModel request)
        {
            var users = await _identityService.GetUsersAsync(request);
            return users;
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var responseModel = await _identityService.GetByIdAsync(id);
            return new CustomActionResult(responseModel);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var responseModel = await _identityService.LoginAsync(model);
            return new CustomActionResult(responseModel);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var responseModel = await _identityService.GetProfileAsync(phoneNumber);
            return new CustomActionResult(responseModel);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var responseModel = await _identityService.RegisterAsync(model);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Disabse(string id, [FromBody] DisableUserRequest request)
        {
            request.UserId = id;
            var responseModel = await _identityService.DisableUserAsync(request);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel request)
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.PhoneNumber = phoneNumber;
            var responseModel = await _identityService.ChangePasswordAsync(request);
            return new CustomActionResult(responseModel);
        }
    }
}
