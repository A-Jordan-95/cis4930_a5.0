using CS_ABET.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS_ABET.Persistence
{
    public static class DbMock
    {
        public static List<Semester> Semesters = new List<Semester> { 
            new Semester{Id = 1, Name = "Fall2019"},
            new Semester{Id = 2, Name = "Spring2020"},
            new Semester{Id = 3, Name = "Summer2020"}
        };
    }
}