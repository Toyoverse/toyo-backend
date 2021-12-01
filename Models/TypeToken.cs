using System;

namespace BackendToyo.Models
{
    public class TypeToken
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MetaDataUrl { get; set; }
        public Int64 TypeId { get; set; }
    }
}