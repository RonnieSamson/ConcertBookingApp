﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Concert.MAUI.Services
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;

        public RestService(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Response: {content}"); // Logga JSON-svaret

                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data)
        {
            var response = await _client.PostAsJsonAsync(uri, data);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content)!;
        }

        public async Task<T> PutAsync<T>(string uri, T data)
        {
            var response = await _client.PutAsJsonAsync(uri, data);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content)!;
        }

        public async Task DeleteAsync(string uri)
        {
            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}
