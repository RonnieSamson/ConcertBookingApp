using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Entity
{
    public class Concert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Performance> Performances { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
