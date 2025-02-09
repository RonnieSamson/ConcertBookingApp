using AutoMapper;
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
    public class ConcertService : IConcertService
    {
        private readonly IRestService _restService;
        private readonly IMapper _mapper;

        public ConcertService(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }

        public async Task<List<ConcertDto>> GetConcertsAsync()
        {
            // Hämta konserter direkt som DTO från API:t
            return await _restService.GetAsync<List<ConcertDto>>("api/concerts");
        }
        

        public async Task<ConcertDto> GetConcertAsync(int id)
        {
            // Hämta en specifik konsert som DTO
            var concertDto = await _restService.GetAsync<ConcertDto>($"api/concerts/{id}");

            // Om ingen konsert hittas, returnera null
            if (concertDto == null)
                return null;

            // Mappa DTO till lokal modell
            return _mapper.Map<ConcertDto>(concertDto);
        }

        public async Task AddConcertAsync(ConcertDto concert)
        {
            // Mappa från lokalt Concert-objekt till DTO
            var concertDto = _mapper.Map<ConcertDto>(concert);

            // Skicka POST-anrop via RestService
            await _restService.PostAsync("api/concerts", concertDto);
        }

        public async Task UpdateConcertAsync(ConcertDto concert)
        {
            // Mappa från lokalt Concert-objekt till DTO
            var concertDto = _mapper.Map<ConcertDto>(concert);

            // Skicka PUT-anrop via RestService
            await _restService.PutAsync($"api/concerts/{concert.Id}", concertDto);
        }

        public async Task DeleteConcertAsync(int concertId)
        {
            // Skicka DELETE-anrop via RestService
            await _restService.DeleteAsync($"api/concerts/{concertId}");
        }

        public async Task<IEnumerable<ConcertDto>> SearchConcertsAsync(string searchTerm)
        {
            // Hämta alla konserter från API
            var concertDtos = await _restService.GetAsync<List<ConcertDto>>("api/concerts");

            // Mappa DTO till lokala modeller
            var concerts = _mapper.Map<List<ConcertDto>>(concertDtos);

            // Filtrera lokalt baserat på sökterm
            return concerts.Where(c => c.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}
