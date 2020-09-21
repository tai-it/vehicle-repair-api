using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using VehicleRepairs.Api.Services.Station.Models;
using VehicleRepairs.Shared.Common;
using Xunit;

namespace VehicleRepairs.IntegrationTests
{
    public class StationTests : IntegrationTest
    {
        [Fact]
        public async Task GetListStations()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/stations?limit=10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
