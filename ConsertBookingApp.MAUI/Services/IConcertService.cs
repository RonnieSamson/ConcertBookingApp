using ConsertBookingApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsertBookingApp.MAUI.Services
{
    public interface IConcertService
    {
        Task<IEnumerable<Concert>> GetConcertsAsync();
        Task<Concert> GetConcertAsync(int id);
        Task AddConcertAsync(Concert concert);
        Task UpdateConcertAsync(Concert concert);
        Task DeleteConcertAsync(int concertId);
    }
}
