using CS_ABET.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS_ABET.Persistence
{
    public static class DbMock
    {
        public static List<Subject> Subjects = new List<Subject> { 
            new Subject{Id = 1, Name = "Fall2019"},
            new Subject{Id = 2, Name = "Spring2020"},
            new Subject{Id = 3, Name = "Summer2020"}
        };
    }
}