using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace CS_ABET.Persistence.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }
    }
}