using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entity
{
    public class User
    {
        [Key]

        [MinLength(3)]
        public required string ID { get; set; }
        [StringLength(30)]
        public required string Name { get; set; }
        [StringLength(50)]
        public required string Email { get; set; }
        [StringLength(30)]
        [MinLength(6)]
        public required string Password { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
