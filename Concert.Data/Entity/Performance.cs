namespace Concert.Data.Entity
{
    public class Performance
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ConcertId { get; set; }
        public ConcertEntity Concert { get; set; }
    }
}
