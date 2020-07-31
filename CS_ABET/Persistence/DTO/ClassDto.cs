using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS_ABET.Persistence.DTO
{
    public class ClassDto
    {
        public int Id { get; set; }

        public int SemesterId { get; set; }

        public string Instructor { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }
    }
}