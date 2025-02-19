﻿using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public class ConcertRepository : Repository<ConcertEntity>, IConcertRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public ConcertRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ConcertEntity?> GetConcertByIdAsync(string id)
        {
            return (await Find(c => c.ConcertId == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<ConcertEntity>> GetConcertsAsync()
        {
            return await All();
        }

        public void AddConcert(ConcertEntity concert)
        {
            Insert(concert);
        }

        public void UpdateConcert(ConcertEntity concert)
        {
            Update(concert);
        }

        public void DeleteConcert(ConcertEntity concert)
        {
            Delete(concert);
        }
    }
}
