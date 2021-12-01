using System;

namespace BackendToyo.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public Int64 NFTId { get; set; }
        public Guid TypeId { get; set; }
    }
}