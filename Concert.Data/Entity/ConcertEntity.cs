using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entity
{
    public class ConcertEntity
    {
        [Key]
        public required string ConcertId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public ICollection<Performance> Performances { get; set; } = new List<Performance>();
    }
}