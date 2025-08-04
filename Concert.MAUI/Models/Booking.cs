namespace Concert.MAUI.Models
{
    public class Booking
    {
        public string Id { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string PerformanceId { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public string? PerformanceName { get; set; } // ✅ KRÄVS: Association information
        
        // Navigation property for display purposes
        public Performance? Performance { get; set; }
    }
}
