namespace Concert.Data.Entity
{
    public class Concert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
