using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Entity
{
    public class Performance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string location { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
    }
}
