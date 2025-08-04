using AutoMapper;
using Concert.Data.DTO;
using Concert.MAUI.Models;

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

        public async Task<List<Concert.MAUI.Models.Concert>?> GetAllConcertsAsync()
        {
            // Hämta DTOs från API
            var concertDtos = await _restService.GetAsync<List<ConcertDto>>("concerts");
            if (concertDtos == null) return null;

            // Konvertera till MAUI Models
            return _mapper.Map<List<Concert.MAUI.Models.Concert>>(concertDtos);
        }

        public async Task<Concert.MAUI.Models.Concert?> GetConcertByIdAsync(string id)
        {
            // Hämta DTO från API
            var concertDto = await _restService.GetAsync<ConcertDto>($"concerts/{id}");
            if (concertDto == null) return null;

            // Konvertera till MAUI Model
            return _mapper.Map<Concert.MAUI.Models.Concert>(concertDto);
        }

        public async Task SaveConcertAsync(Concert.MAUI.Models.Concert concert, bool isNewConcert)
        {
            var concertDto = _mapper.Map<ConcertDto>(concert);

            if (isNewConcert)
                await _restService.PostAsync<Concert.MAUI.Models.Concert>("concerts", concertDto);
            else
                await _restService.PutAsync<Concert.MAUI.Models.Concert>($"concerts/{concert.ConcertId}", concertDto);
        }

        public async Task DeleteConcertAsync(string id)
        {
            await _restService.DeleteAsync($"concerts/{id}");
        }
    }
}
