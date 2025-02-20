using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public interface IConcertService
    {
        Task<List<Concert.MAUI.Models.Concert>?> GetAllConcertsAsync();
        Task<Concert.MAUI.Models.Concert?> GetConcertByIdAsync(string id);
        Task SaveConcertAsync(Concert.MAUI.Models.Concert concert, bool isNewConcert);
        Task DeleteConcertAsync(string id);
    }
}
