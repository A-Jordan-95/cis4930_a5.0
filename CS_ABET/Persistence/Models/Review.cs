using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace CS_ABET.Persistence.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int RatingOutOfFive { get; set; }

        public string ReviewText { get; set; }
    }
}