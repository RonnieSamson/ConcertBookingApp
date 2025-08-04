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
            // Hämta DTOs från API
            var performanceDtos = await _restService.GetAsync<List<PerformanceDto>>($"Performances/byConcert/{concertId}");
            if (performanceDtos == null) return null;

            // Konvertera till MAUI Models
            return _mapper.Map<List<Performance>>(performanceDtos);
        }

        public async Task<Performance?> GetPerformanceByIdAsync(string id)
        {
            // Hämta DTO från API
            var performanceDto = await _restService.GetAsync<PerformanceDto>($"Performances/{id}");
            if (performanceDto == null) return null;

            // Konvertera till MAUI Model
            return _mapper.Map<Performance>(performanceDto);
        }
    }
}
