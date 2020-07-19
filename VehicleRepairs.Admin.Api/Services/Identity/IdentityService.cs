namespace VehicleRepairs.Admin.Api.Services.Identity
{
    using System.Threading.Tasks;
    using VehicleRepairs.Admin.Api.Services.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using VehicleRepairs.Admin.Api.Infrastructure.Utilities;
    using VehicleRepairs.Shared.Common;
    using VehicleRepairs.Database.Domain.Entities;

    public interface IIdentityService<T>
    {
        Task<ResponseModel> LoginAsync(LoginDto model);

        Task<ResponseModel> RegisterAsync(RegisterDto model);

        Task<ResponseModel> DisableUserAsync(DisableUserRequest request);

        Task<PagedList<UserBaseViewModel>> GetUsersAsync(BaseRequestModel request);
    }

    public class IdentityService : IIdentityService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ResponseModel> LoginAsync(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.PhoneNumber == model.PhoneNumber);
                var roles = await _userManager.GetRolesAsync(appUser);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = GenerateJwtToken(appUser, roles.ToArray())
                };
            }
            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Thông tin tài khoản hoặc mật khẩu không chính xác"
            };
        }

        public async Task<ResponseModel> RegisterAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.PhoneNumber,
                Email = model.Email ?? null,
                Name = model.Name ?? null,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address ?? null,
                DeviceToken = model.DeviceToken ?? null,
                PhoneNumberConfirmed = model.PhoneNumberConfirmed
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, model.Role);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = GenerateJwtToken(user, new string[] { model.Role })
                };
            }

            var errors = result.Errors;

            var message = string.Empty;

            foreach (var error in errors)
            {
                message += error.Description;
            }

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = message
            };
        }

        public async Task<ResponseModel> DisableUserAsync(DisableUserRequest request)
        {
            var user = await _userManager.Users.Where(x => x.Id == request.UserId && x.IsActive).FirstOrDefaultAsync();

            if (user == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Tài khoản này không tìm thấy hoặc đã bị khoá"
                };
            }

            user.IsActive = request.IsActive;
            await _userManager.UpdateAsync(user);

            // SEND NOTIFICATION HERE

            //

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = request.IsActive ? "Mở khoá tài khoản thành công" : "Khoá tài khoản thành công"
            };
        }

        public async Task<PagedList<UserBaseViewModel>> GetUsersAsync(BaseRequestModel request)
        {
            var list = await this._userManager.Users
                    .Where(x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query)))
                    .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role)
                            .Select(x => new UserBaseViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "CreatedOn";
            }

            var viewModelType = typeof(UserBaseViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<UserBaseViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var viewModel = new UserBaseViewModel();
            var type = viewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        private object GenerateJwtToken(IdentityUser user, string[] roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.PhoneNumber)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
