using System.Text.Json.Serialization;

namespace Concert.Data.Entity
{
    public class Performance
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Koppling till ConcertEntity
        public string ConcertId { get; set; }
        
        public ConcertEntity Concert { get; set; }

        // Koppling till Booking
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}