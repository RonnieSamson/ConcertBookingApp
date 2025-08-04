using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public interface IConcertRepository : IRepository<ConcertEntity>
    {
        Task<ConcertEntity?> GetConcertByIdAsync(string id);
        Task<IEnumerable<ConcertEntity>> GetConcertsAsync();
        void AddConcert(ConcertEntity concert);
        void UpdateConcert(ConcertEntity concert);
        void DeleteConcert(ConcertEntity concert);
    }
}
