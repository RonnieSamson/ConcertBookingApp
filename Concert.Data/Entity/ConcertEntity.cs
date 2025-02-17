using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entity
{
    public class ConcertEntity
    {
        [Key]
        public string ConcertId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Performance> Performances { get; set; }
    }
}
