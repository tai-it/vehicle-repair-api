using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairs.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            TestClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-vrs.herokuapp.com")
            };
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetAccessTokenAsync());
        }

        protected StringContent BuildStringContent<T>(T obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var response = await TestClient.PostAsync(
                    "api/account/login",
                    BuildStringContent(new
                    {
                        phoneNumber = "0987654322",
                        password = "P@$$w0rd"
                    })
                );

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }
    }
}