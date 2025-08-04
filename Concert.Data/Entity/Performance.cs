using System.Text.Json.Serialization;

namespace Concert.Data.Entity
{
    public class Performance
    {
        public required string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Location { get; set; } // ✅ KRÄVS: Location för varje Performance

        // Koppling till ConcertEntity
        public required string ConcertId { get; set; }
        
        public ConcertEntity? Concert { get; set; }

        // Koppling till Booking
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}