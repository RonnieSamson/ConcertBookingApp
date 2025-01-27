using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.DTO;
using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public interface IConcertService
    {
        Task<IEnumerable<Models.Concerts>> GetConcertsAsync();
        Task<Models.Concerts> GetConcertAsync(int id);
        Task AddConcertAsync(Models.Concerts concert);
        Task UpdateConcertAsync(Models.Concerts concert);
        Task DeleteConcertAsync(int concertId);
    }
}
