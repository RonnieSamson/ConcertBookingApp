﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concert.Data.Entity
{
    internal class User
    {
        [Key]
        [StringLength(36)]
        [MinLength(10)]
        public required string ID { get; set; }
        [StringLength(30)]
        public required string name { get; set; }
        [StringLength(50)]
        public required string Email { get; set; }
        [StringLength(30)]
        [MinLength(6)]
        public required string Password { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
