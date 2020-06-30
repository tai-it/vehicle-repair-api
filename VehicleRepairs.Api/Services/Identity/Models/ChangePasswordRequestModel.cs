namespace VehicleRepairs.Api.Services.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordRequestModel
    {
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu cũ là trường bắt buộc")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới là trường bắt buộc")]
        [StringLength(100, ErrorMessage = "Mật khẩu mới không hợp lệ (8 - 100 kí tự)", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu mới phải chứa ít nhất một chữ hoa, chữ thường, số và kí tự đặc biệt")]
        public string NewPassword { get; set; }

        public bool LogoutOnOtherDevices { get; set; }
    }
}
