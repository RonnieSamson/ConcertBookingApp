namespace Concert.MAUI.Models
{
    public class Concert
    {
        public string ConcertId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
