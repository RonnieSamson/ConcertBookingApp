﻿namespace Concert.Data.DTO
{
    public class PerformanceDto
    {
        public string Id { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
