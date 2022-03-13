using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendToyo.Models
{
    [Table("TypeToken")]
    public class TypeToken
    {
        [Column("TypeId")]
        public int Id { get; set; }

        [Column("ChainId")]
        public string ChainId { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Type")]
        public string Type { get; set; }
    }
}