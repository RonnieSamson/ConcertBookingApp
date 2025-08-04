namespace Concert.Data.Entity
{
    public class Booking
    {
        public string Id { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }

        // Koppling till Performance
        public required string PerformanceId { get; set; }
        public Performance? Performance { get; set; }

        // Customer information
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }

        

        
    }
}