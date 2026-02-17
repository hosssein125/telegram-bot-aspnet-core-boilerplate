using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Models.ThreeXUi;

namespace TelegramBot.Infrastructure.Services
{
    public class ThreeXUiService : IThreeXUiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ThreeXUiService> _logger;
        private readonly string _baseUrl;

        public ThreeXUiService(HttpClient httpClient, IConfiguration configuration, ILogger<ThreeXUiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _baseUrl = configuration["ThreeXUi:Url"]?.TrimEnd('/') ?? throw new ArgumentNullException("ThreeXUi:Url configuration is missing");
        }

        public async Task<bool> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var loginData = new { username, password };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/login", loginData, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Login failed with status code: {StatusCode}", response.StatusCode);
                    return false;
                }

                var content = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken);
                return content?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                return false;
            }
        }

        public async Task<List<InboundDto>> GetInboundsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/panel/api/inbounds/list", null, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<GenericResponse<List<InboundDto>>>(cancellationToken: cancellationToken);
                return content?.Obj ?? new List<InboundDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching inbounds");
                return new List<InboundDto>();
            }
        }

        public async Task<InboundDto?> GetInboundAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/panel/api/inbounds/get/{id}", null, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<GenericResponse<InboundDto>>(cancellationToken: cancellationToken);
                return content?.Obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching inbound {Id}", id);
                return null;
            }
        }

        public async Task<bool> AddInboundAsync(InboundDto inbound, CancellationToken cancellationToken = default)
        {
            try
            {
                // API expects form-urlencoded sometimes or JSON. 
                // Based on standard 3x-ui, it's usually POST with JSON or Form. 
                // Let's try JSON first as it's modern.
                // Note: The 'settings', 'streamSettings', 'sniffing' in DTO are strings. 
                // The API might expect them as actual JSON objects or strings depending on implementation.
                // However, the search result 4 says: "settings JSON field".
                // Often the API wrapper sends them as objects. 
                // But let's check how we serialize. 
                
                // If the API expects the nested fields to be objects, we might need to adjust DTO or serialization.
                // But for now, let's assume the API accepts the structure we defined or we need to send them as FormUrlEncoded.
                // Many x-ui forks use FormUrlEncoded for add/update.
                
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("up", inbound.Up.ToString()),
                    new KeyValuePair<string, string>("down", inbound.Down.ToString()),
                    new KeyValuePair<string, string>("total", inbound.Total.ToString()),
                    new KeyValuePair<string, string>("remark", inbound.Remark),
                    new KeyValuePair<string, string>("enable", inbound.Enable.ToString().ToLower()),
                    new KeyValuePair<string, string>("expiryTime", inbound.ExpiryTime.ToString()),
                    new KeyValuePair<string, string>("listen", inbound.Listen),
                    new KeyValuePair<string, string>("port", inbound.Port.ToString()),
                    new KeyValuePair<string, string>("protocol", inbound.Protocol),
                    new KeyValuePair<string, string>("settings", inbound.Settings),
                    new KeyValuePair<string, string>("streamSettings", inbound.StreamSettings),
                    new KeyValuePair<string, string>("tag", inbound.Tag),
                    new KeyValuePair<string, string>("sniffing", inbound.Sniffing)
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync($"{_baseUrl}/panel/api/inbounds/add", content, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to add inbound. Status: {Status}, Error: {Error}", response.StatusCode, error);
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<GenericResponse<object>>(cancellationToken: cancellationToken);
                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding inbound");
                return false;
            }
        }

        public async Task<bool> UpdateInboundAsync(int id, InboundDto inbound, CancellationToken cancellationToken = default)
        {
            try
            {
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("up", inbound.Up.ToString()),
                    new KeyValuePair<string, string>("down", inbound.Down.ToString()),
                    new KeyValuePair<string, string>("total", inbound.Total.ToString()),
                    new KeyValuePair<string, string>("remark", inbound.Remark),
                    new KeyValuePair<string, string>("enable", inbound.Enable.ToString().ToLower()),
                    new KeyValuePair<string, string>("expiryTime", inbound.ExpiryTime.ToString()),
                    new KeyValuePair<string, string>("listen", inbound.Listen),
                    new KeyValuePair<string, string>("port", inbound.Port.ToString()),
                    new KeyValuePair<string, string>("protocol", inbound.Protocol),
                    new KeyValuePair<string, string>("settings", inbound.Settings),
                    new KeyValuePair<string, string>("streamSettings", inbound.StreamSettings),
                    new KeyValuePair<string, string>("tag", inbound.Tag),
                    new KeyValuePair<string, string>("sniffing", inbound.Sniffing)
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync($"{_baseUrl}/panel/api/inbounds/update/{id}", content, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to update inbound. Status: {Status}, Error: {Error}", response.StatusCode, error);
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<GenericResponse<object>>(cancellationToken: cancellationToken);
                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating inbound {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteInboundAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/panel/api/inbounds/del/{id}", null, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to delete inbound. Status: {Status}, Error: {Error}", response.StatusCode, error);
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<GenericResponse<object>>(cancellationToken: cancellationToken);
                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting inbound {Id}", id);
                return false;
            }
        }
    }
}
