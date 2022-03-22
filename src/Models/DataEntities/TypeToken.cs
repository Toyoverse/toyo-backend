using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendToyo.Models.DataEntities
{
    public class TypeToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ChainId { get; set; }

        public BoxType BoxType { get; set; } 
    }
}