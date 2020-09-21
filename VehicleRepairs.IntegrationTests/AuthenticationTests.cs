using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using VehicleRepairs.Api.Services.Identity.Models;
using Xunit;

namespace VehicleRepairs.IntegrationTests
{
    public class AuthenticationTests : IntegrationTest
    {
        [Fact]
        public async Task Login_With_ValidAccount()
        {
            // Act
            var response = await TestClient.PostAsync(
                    "api/account/login",
                    BuildStringContent(new
                    {
                        phoneNumber = "0987654322",
                        password = "P@$$w0rd"
                    })
                );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var token = await response.Content.ReadAsStringAsync();

            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Login_With_InvalidPayload()
        {
            // Act
            var response = await TestClient.PostAsync("api/account/login", BuildStringContent(new { }));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        //[Fact]
        //public async Task Register_With_ValidPayload()
        //{
        //    // Arrange
        //    Random r = new Random();

        //    // Act
        //    var response = await TestClient.PostAsync(
        //            "api/account/register",
        //            BuildStringContent(new
        //            {
        //                name = "Integration Test",
        //                phoneNumber = string.Concat("09", r.Next(10000000, 90000000)),
        //                password = "P@$$w0rd",
        //                role = "User",
        //                phoneNumberConfirmed = false
        //            })
        //        );

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var token = await response.Content.ReadAsStringAsync();

        //    token.Should().NotBeNullOrEmpty();
        //}

        [Fact]
        public async Task GetProfile_Without_Authorize()
        {
            // Act
            var response = await TestClient.GetAsync("api/account/me");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetProfile_With_ValidAccount()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/account/me");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<UserBaseViewModel>(await response.Content.ReadAsStringAsync());

            user.Should().NotBeNull();
            user.PhoneNumber.Should().Be("0987654322");
        }
    }
}
