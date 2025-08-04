namespace Concert.MAUI.Models
{
    public class Concert
    {
        public required string ConcertId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int BookingCount { get; set; } // ✅ KRÄVS: Antal bokningar för konserter
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
