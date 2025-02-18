using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UserService.Services
{
    public class OrderServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderServiceClient> _logger;

        public OrderServiceClient(HttpClient httpClient, ILogger<OrderServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetOrdersByUserId(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/orders/byUser/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                _logger.LogWarning($"OrderService returned {response.StatusCode} for userId {userId}");
                return "[]"; // Return empty array in case of failure
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error calling OrderService: {ex.Message}");
                return "[]";
            }
        }
    }
}
