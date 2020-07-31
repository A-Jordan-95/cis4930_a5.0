using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CS_ABET.Controllers.Enterprise
{
    public class ClassEC : BaseEC
    {
        public async Task<IEnumerable<ClassDto>> Get(int SemesterId)
        {
            var results = new List<ClassDto>();
            using (var db = new AbetContext())
            {
                try
                {
                    results = await db.Classes.Where(s => s.SemesterId == SemesterId)
                    .Select(s => 
                    new ClassDto { Id = s.Id, 
                        SemesterId = s.SemesterId,
                        CourseCode = s.CourseCode,
                        CourseName = s.CourseName,
                        Instructor = s.Instructor, }).ToListAsync();
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        public async Task<ClassDto> AddOrUpdate(ClassDto classDto)
        {
            using (var db = new AbetContext())
            {
                var c = new Class
                {
                    Id = classDto.Id,
                    SemesterId = classDto.SemesterId,
                    CourseCode = classDto.CourseCode,
                    CourseName = classDto.CourseName,
                    Instructor = classDto.Instructor,
                };
                db.Classes.AddOrUpdate(c);
                await db.SaveChangesAsync();

                return new ClassDto
                {
                    Id = c.Id,
                    SemesterId = c.SemesterId,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    Instructor = c.Instructor
                };
            }
        }

        public async Task<ClassDto> Remove(ClassDto classDto)
        {
            using (var db = new AbetContext())
            {
                var result = db.Classes.Remove(new Class
                {
                    Id = classDto.Id,
                    SemesterId = classDto.SemesterId,
                    CourseCode = classDto.CourseCode,
                    CourseName = classDto.CourseName,
                    Instructor = classDto.Instructor
                });
                db.SaveChanges();
                return new ClassDto { Id = result.Id,
                        SemesterId = result.SemesterId,
                        CourseCode = result.CourseCode,
                        CourseName = result.CourseName,
                        Instructor = result.Instructor  };
            }
        }

        public async Task<ClassDto> Remove(int id)
        {
            using(var db = new AbetContext())
            {
                var c = db.Classes.FirstOrDefault(cl => cl.Id == id);
                db.Classes.Remove(c);
                await db.SaveChangesAsync();

                return new ClassDto
                {
                    Id = c.Id,
                    SemesterId = c.SemesterId,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    Instructor = c.Instructor
                }; ;
            }
        }
    }
}