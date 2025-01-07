using ConcertBookingApp.Data.DTO;
using ConsertBookingApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsertBookingApp.MAUI.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IRestService _restService;
        private readonly AutoMapper.IMapper _mapper;

        public ConcertService(IRestService restService, AutoMapper.IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }

        public async Task<Concert> GetConcertAsync(int id)
        {
            var concertDto = await _restService.GetAsync<ConcertDto>($"api/concerts/{id}");
            // Mappa från ConcertDto till Concert
            return _mapper.Map<Concert>(concertDto);
        }

        public async Task<IEnumerable<Concert>> GetConcertsAsync()
        {
            var concertDtos = await _restService.GetAsync<IEnumerable<ConcertDto>>("api/concerts");
            // Mappa från ConcertDto till Concert
            return _mapper.Map<IEnumerable<Concert>>(concertDtos);
        }

        public async Task AddConcertAsync(Concert concert)
        {
            // Mappa från Concert till ConcertDto
            var concertDto = _mapper.Map<ConcertDto>(concert);

            await _restService.PostAsync("api/concerts", concertDto);
        }

        public async Task UpdateConcertAsync(Concert concert)
        {
            // Mappa från Concert till ConcertDto
            var concertDto = _mapper.Map<ConcertDto>(concert);

            await _restService.PutAsync($"api/concerts/{concert.Id}", concertDto);
        }

        public async Task DeleteConcertAsync(int concertId)
        {
            await _restService.DeleteAsync($"api/concerts/{concertId}");
        }
       
    }
}
