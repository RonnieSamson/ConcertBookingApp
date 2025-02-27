﻿using System.Text.Json.Serialization;

namespace Concert.Data.Entity
{
    public class Performance
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ConcertId { get; set; }
        
        [JsonIgnore]
        public ConcertEntity Concert { get; set; }
    }
}
