using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.MAUI.Models;
using Concert.Data.DTO;
using AutoMapper;
using System.Text.Json;

namespace Concert.MAUI.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRestService _restService;
        private readonly IMapper _mapper;

        public PerformanceService(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }
        
        public async Task<List<Performance>?> GetPerformancesByConcertIdAsync(string concertId)
        {
            System.Diagnostics.Debug.WriteLine($"🎭 PerformanceService.GetPerformancesByConcertIdAsync called with concertId: '{concertId}'");
            
            if (string.IsNullOrEmpty(concertId))
            {
                System.Diagnostics.Debug.WriteLine("❌ ConcertId is null or empty - returning null");
                return null;
            }
            
            // Hämta DTOs från API
            var performanceDtos = await _restService.GetAsync<List<PerformanceDto>>($"Performances/byConcert/{concertId}");
            if (performanceDtos == null) return null;

            // Konvertera till MAUI Models
            return _mapper.Map<List<Performance>>(performanceDtos);
        }

        public async Task<Performance?> GetPerformanceByIdAsync(string id)
        {
            System.Diagnostics.Debug.WriteLine($"🎪 PerformanceService.GetPerformanceByIdAsync called with id: '{id}'");
            
            if (string.IsNullOrEmpty(id))
            {
                System.Diagnostics.Debug.WriteLine("❌ Performance ID is null or empty - returning null");
                return null;
            }
            
            // Hämta DTO från API
            var performanceDto = await _restService.GetAsync<PerformanceDto>($"Performances/{id}");
            if (performanceDto == null) return null;

            // Konvertera till MAUI Model
            return _mapper.Map<Performance>(performanceDto);
        }
    }
}
