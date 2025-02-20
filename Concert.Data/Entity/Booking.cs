namespace Concert.Data.Entity
{
    public class Booking
    {

        public string Id { get; set; }

        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ConcertId { get; set; }
        public ConcertEntity Concert { get; set; }
    }
}
