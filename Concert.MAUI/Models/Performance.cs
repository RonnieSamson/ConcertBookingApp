namespace Concert.MAUI.Models
{
    public class Performance
    {
        public string Id { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = string.Empty; // ✅ KRÄVS: Location för varje Performance
        public string ConcertId { get; set; } = string.Empty; // ✅ Association med Concert
        public int BookingCount { get; set; } // ✅ KRÄVS: Antal bokningar
    }
}
