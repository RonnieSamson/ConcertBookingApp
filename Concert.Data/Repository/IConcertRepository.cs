using Concert.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.Data.Repository
{
    public interface IConcertRepository : IRepository<ConcertEntity>
    {
        Task<ConcertEntity> GetConcertByIdAsync(string id);
        Task<IEnumerable<ConcertEntity>> GetConcertsAsync();
        void AddConcert(ConcertEntity concert);
        void UpdateConcert(ConcertEntity concert);
        void DeleteConcert(ConcertEntity concert);
    }
}
