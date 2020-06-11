namespace VehicleRepairs.Api.Services.Identity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class LoginDto
    {
        private string _phoneNumber;

        [Required]
        [Phone]
        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = Regex.Replace(value, @"\s+", ""); }
        }

        [Required]
        public string Password { get; set; }
    }
}
