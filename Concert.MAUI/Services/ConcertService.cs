using Concert.DTO;
using Concert.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Concert.MAUI.Services
{
    public class ConcertService
    {
        private readonly HttpClient _httpClient;

        public ConcertService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Concerts>> GetConcertsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Concerts>>("api/concerts");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw;
            }
        }
    }
}
