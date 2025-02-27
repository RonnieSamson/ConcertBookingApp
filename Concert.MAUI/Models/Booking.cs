﻿namespace Concert.MAUI.Models
{
    public class Booking
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public string ConcertId { get; set; } = null!;
    }
}
