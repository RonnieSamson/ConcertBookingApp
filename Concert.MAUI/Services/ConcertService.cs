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
            return await _restService.GetAsync<List<Concert.MAUI.Models.Concert>>("concerts");
        }

        public async Task<Concert.MAUI.Models.Concert?> GetConcertByIdAsync(string id)
        {
            return await _restService.GetAsync<Concert.MAUI.Models.Concert>($"concerts/{id}");
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
