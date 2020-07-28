using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CS_ABET.Persistence.Models
{
    [Table("dbo.Semester")]
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        
    }
}