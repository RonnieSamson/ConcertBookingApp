using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.Data;

namespace Concert.Data.DTO
{
    public class BookingDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
       
        public string Email { get; set; } = null!;
        public string UserId { get; set; } = null!;
     
      
    }
}
