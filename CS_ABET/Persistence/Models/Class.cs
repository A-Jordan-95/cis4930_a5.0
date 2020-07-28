using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CS_ABET.Persistence.Models
{
    [Table("Class")]
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public string Instructor { get; set; }

        public byte[] Syllabus { get; set; }

        public string CanvasLink { get; set; }

        public int Enrollment { get; set; }
    }
}