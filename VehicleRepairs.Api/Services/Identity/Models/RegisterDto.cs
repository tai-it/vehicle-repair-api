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

        [Required(ErrorMessage = "Tên là trường bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên không hợp lệ", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Số điện thoại là trường bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(11, ErrorMessage = "Số điện thoại không hợp lệ", MinimumLength = 10)]
        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = Regex.Replace(value, @"\s+", ""); }
        }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là trường bắt buộc")]
        [StringLength(100, ErrorMessage = "Mật khẩu không hợp lệ (8 - 100 kí tự)", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ hoa, chữ thường, số và kí tự đặc biệt")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vai trò là trường bắt buộc")]
        public string Role { get; set; }

        public string Address { get; set; }

        public string DeviceToken { get; set; }

        [Required(ErrorMessage = "Tình trạng số điện thoại là trường bắt buộc")]
        public bool PhoneNumberConfirmed { get; set; }

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
