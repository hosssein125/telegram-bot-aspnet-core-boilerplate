using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using TelegramBot.Application.Common.Interfaces;

namespace TelegramBot.Infrastructure.Services
{
    public class IbsNgService : IIbsNgService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IbsNgService> _logger;
        private readonly string _baseUrl;

        public IbsNgService(HttpClient httpClient, ILogger<IbsNgService> logger, string baseUrl = "http://localhost:8080") // Default URL, should come from config
        {
            _httpClient = httpClient;
            _logger = logger;
            _baseUrl = baseUrl;
        }

        private async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{endpoint}", data);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"IBSng API Error ({endpoint}): {response.StatusCode} - {error}");
                    return default;
                }

                var result = await response.Content.ReadFromJsonAsync<IbsNgResponse<T>>();
                if (result?.IsError == true)
                {
                    _logger.LogError($"IBSng Logic Error ({endpoint}): {result.Message}");
                    return default;
                }

                return result != null ? result.Data : default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception calling IBSng ({endpoint})");
                return default;
            }
        }

        public async Task<long> AddAccountAsync(string username, string password, int groupId, double credit, long? firstLogin = null)
        {
            var data = new { username, password, group_id = groupId, credit, first_login = firstLogin };
            var result = await PostAsync<long>("addAccount", data);
            return result;
        }

        public async Task<bool> LockAccountAsync(string username, string password)
        {
            var data = new { username, password };
            var result = await PostAsync<long>("lockAccount", data); // Returns user_id on success
            return result > 0;
        }

        public async Task<bool> UnlockAccountAsync(string username, string password)
        {
            var data = new { username, password };
            var result = await PostAsync<long>("unLockAccount", data);
            return result > 0;
        }

        public async Task<bool> RenewAccountAsync(string username, string password, int groupId, double credit)
        {
            var data = new { username, password, group_id = groupId, credit };
            // renewAccount returns success message string, not data in some cases, need to handle generic success
            // Assuming wrapper handles boolean logic based on null return
             try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/renewAccount", data);
                var result = await response.Content.ReadFromJsonAsync<IbsNgResponse<object>>();
                return result?.IsError == false;
            }
            catch { return false; }
        }

        public async Task<bool> EditAccountAsync(string username, string password, string newPassword = null, string newUsername = null)
        {
             // Note: The PHP API has /editAccount for changing username/password
             // But signature in PHP is just username, password, group_id? No, it updates to the SAME username/password if not changed?
             // Actually PHP code: update normal_users set normal_username='$username',normal_password='$password' where user_id=$user_id
             // It seems it takes new credentials as input for update? The PHP code is a bit ambiguous on "old" vs "new".
             // Assuming we use changeAccountPassword for password change.
             
             if(!string.IsNullOrEmpty(newPassword))
             {
                 var data = new { username, password, newPassword };
                 var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/changeAccountPassword", data);
                 var result = await response.Content.ReadFromJsonAsync<IbsNgResponse<object>>();
                 return result?.IsError == false;
             }
             return false;
        }

        public async Task<bool> GiftUserCreditAndTimeAsync(string username, string password, double credit, int days)
        {
            var data = new { username, password, credit, day = days };
             var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/giftUserCreditAndTime", data);
             var result = await response.Content.ReadFromJsonAsync<IbsNgResponse<object>>();
             return result?.IsError == false;
        }

        public async Task<bool> CheckUserExistsAsync(string username, string password)
        {
            var data = new { username, password };
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/checkUsernameAndPassword", data);
            var result = await response.Content.ReadFromJsonAsync<IbsNgResponse<object>>();
            return result?.IsError == false;
        }

        public async Task<long> GetUserIdAsync(string username, string password)
        {
             var data = new { username, password };
             return await PostAsync<long>("getUserId", data);
        }

        public async Task<object> GetUserInfoAsync(string username, string password)
        {
             // PHP: getUserInfos
             var data = new { username, password };
             return await PostAsync<object>("getUserInfos", data);
        }
    }

    public class IbsNgResponse<T>
    {
        [JsonPropertyName("is_error")]
        public bool IsError { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
