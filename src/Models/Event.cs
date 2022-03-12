using System;

namespace BackendToyo.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CreateTimeStamp { get; set; }
        public string Description { get; set; }
    }
}