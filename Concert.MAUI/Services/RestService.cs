using AutoMapper;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Concert.MAUI.Services
{
    public class RestService : IRestService
    {
        private HttpClient _client;
        private JsonSerializerOptions _serializerOptions;
        private readonly IHttpsClientHandlerService _httpsClientHandlerService;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;

        public RestService(IHttpsClientHandlerService httpsClientHandlerService, IMapper mapper)
        {
            _mapper = mapper;
            _httpsClientHandlerService = httpsClientHandlerService;

            // 🔥 Dynamiskt väljer rätt URL beroende på plattform
#if ANDROID
            _baseUrl = "https://10.0.2.2:5001/api/"; // Android Emulator
#elif IOS
            _baseUrl = "https://127.0.0.1:5001/api/"; // iOS Simulator
#else
            _baseUrl = "https://localhost:5001/api/"; // Windows/Mac
#endif

#if DEBUG
            HttpMessageHandler? handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            _client = handler != null ? new HttpClient(handler) : new HttpClient();
#else
            _client = new HttpClient();
#endif

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var fullUrl = $"{_baseUrl}{endpoint}";
                Debug.WriteLine($"📡 Fetching: {fullUrl}");

                var response = await _client.GetAsync(fullUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"📄 Response: {content}");

                return JsonSerializer.Deserialize<T>(content, _serializerOptions);
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"🚨 HTTP ERROR: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🚨 GENERAL ERROR: {ex.Message}");
            }
            return default;
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_baseUrl}{endpoint}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseContent, _serializerOptions);
                }
                return default;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
                return default;
            }
        }

        public async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_baseUrl}{endpoint}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseContent, _serializerOptions);
                }
                return default;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
                return default;
            }
        }

        public async Task DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_baseUrl}{endpoint}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }
        }
    }
}
