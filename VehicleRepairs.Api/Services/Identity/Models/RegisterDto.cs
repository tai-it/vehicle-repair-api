namespace VehicleRepairs.Api.Services.Identity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Shared.Common;

    public class RegisterDto : IValidatableObject
    {
        private string _phoneNumber;

        public string Name { get; set; }

        [Required(ErrorMessage = "Số điện thoại là trường bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = Regex.Replace(value, @"\s+", ""); }
        }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là trường bắt buộc")]
        [StringLength(100, ErrorMessage = "Mật khẩu không hợp lệ (8 - 100 kí tự)", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vai trò là trường bắt buộc")]
        public string Role { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Role != CommonConstants.Roles.STATION && Role != CommonConstants.Roles.USER)
            {
                yield return new ValidationResult("Không tìm thấy vai trò này", new string[] { "Role" });
            }

            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var user = db.Users.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

            if (user != null)
            {
                yield return new ValidationResult("Số điện thoại đã được sử dụng bởi tài khoản khác", new string[] { "PhoneNumber" });
            }
        }
    }
}
