namespace Concert.Data.DTO
{
    public class BookingDto
    {
        public string? Id { get; set; }
        public required string PerformanceId { get; set; }
        public required string CustomerName { get; set; } = string.Empty;
        public required string CustomerEmail { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string? PerformanceName { get; set; } // ✅ KRÄVS: Association information
    }
}
