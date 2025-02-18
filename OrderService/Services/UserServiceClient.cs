using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OrderService.Services
{
    public class UserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserServiceClient> _logger;

        public UserServiceClient(HttpClient httpClient, ILogger<UserServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetUserById(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                _logger.LogWarning($"UserService returned {response.StatusCode} for userId {userId}");
                return "{}"; // Return empty JSON object in case of failure
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error calling UserService: {ex.Message}");
                return "{}";
            }
        }
    }
}
