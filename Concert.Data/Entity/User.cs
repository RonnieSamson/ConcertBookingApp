namespace Concert.Data.Entity
{
    public class User
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}