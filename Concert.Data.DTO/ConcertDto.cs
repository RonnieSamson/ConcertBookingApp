namespace Concert.Data.DTO
{
    public class ConcertDto
    {
        public required string ConcertId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int BookingCount { get; set; } // ✅ KRÄVS: Antal bokningar för konserter
    }
}
