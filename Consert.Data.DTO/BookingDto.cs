namespace Concert.Data.DTO
{
    public class BookingDto
    {
        public string? Id { get; set; }
        public required string PerformanceId { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
