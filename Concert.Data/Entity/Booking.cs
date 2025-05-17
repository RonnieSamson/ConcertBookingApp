namespace Concert.Data.Entity
{
    public class Booking
    {
        public string Id { get; set; }
        public DateTime BookingDate { get; set; }

        // Koppling till Performance
        public string PerformanceId { get; set; }
        public Performance Performance { get; set; }

        // Koppling till User
        // Användarens ID som bokningen tillhör
        public string UserId { get; set; }
        public User User { get; set; }

        

        
    }
}