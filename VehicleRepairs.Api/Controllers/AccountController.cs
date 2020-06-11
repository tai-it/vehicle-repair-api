namespace VehicleRepairs.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VehicleRepairs.Api.Domain.Entities;
    using VehicleRepairs.Api.Infrastructure.ActionResults;
    using VehicleRepairs.Api.Infrastructure.Filters;
    using VehicleRepairs.Api.Services.Identity;
    using VehicleRepairs.Api.Services.Identity.Models;

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

        [Authorize]
        [HttpPost("me")]
        public async Task<IActionResult> GetProfile()
        {
            var phoneNumber = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var responseModel = await _identityService.GetProfileAsync(phoneNumber);
            return new CustomActionResult(responseModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var responseModel = await _identityService.LoginAsync(model);
            return new CustomActionResult(responseModel);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var responseModel = await _identityService.RegisterAsync(model);
            return new CustomActionResult(responseModel);
        }

        [Authorize]
        [HttpPost("{phoneNumber}/phoneconfirmed")]
        public async Task<IActionResult> ConfirmPhoneNumber(string phoneNumber)
        {
            var responseModel = await _identityService.ConfirmPhoneNumberAsync(phoneNumber);
            return new CustomActionResult(responseModel);
        }

        [Authorize]
        [HttpPut("{phoneNumber}")]
        public async Task<IActionResult> UpdateProfile(string phoneNumber, [FromBody] UserUpdateModel model)
        {
            var responseModel = await _identityService.UpdateProfileAsync(phoneNumber, model);
            return new CustomActionResult(responseModel);
        }
    }
}
