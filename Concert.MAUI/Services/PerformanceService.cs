using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.MAUI.Models;
using AutoMapper;

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
            return await _restService.GetAsync<List<Performance>>($"Performances/byConcert/{concertId}");
        }

    }
}
