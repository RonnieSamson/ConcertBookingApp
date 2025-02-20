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
        private IHttpsClientHandlerService _httpsClientHandlerService;
        private IMapper _mapper;
        private const string _baseUrl = "https://localhost:5001/api/"; 

        public RestService(IHttpsClientHandlerService httpsClientHandlerService, IMapper mapper)
        {
            _mapper = mapper;
            _httpsClientHandlerService = httpsClientHandlerService;

#if DEBUG
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
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
                var response = await _client.GetAsync($"{_baseUrl}{endpoint}"); 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, _serializerOptions);
                }
                return default;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}"); 
                return default;
            }
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
