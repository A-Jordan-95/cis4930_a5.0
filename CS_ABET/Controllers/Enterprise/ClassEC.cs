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
                        CourseId = s.CourseId, 
                        SemesterId = s.SemesterId, 
                        Enrollment = s.Enrollment, 
                        Instructor = s.Instructor }).ToListAsync();
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
                    CourseId = classDto.CourseId,
                    Instructor = classDto.Instructor,
                    Enrollment = classDto.Enrollment
                };
                db.Classes.AddOrUpdate(c);
                await db.SaveChangesAsync();

                return new ClassDto
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    SemesterId = c.SemesterId,
                    Enrollment = c.Enrollment,
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
                    CourseId = classDto.CourseId,
                    Instructor = classDto.Instructor,
                    Enrollment = classDto.Enrollment
                });
                db.SaveChanges();
                return new ClassDto { Id = result.Id,
                        CourseId = result.CourseId,
                        SemesterId = result.SemesterId,
                        Enrollment = result.Enrollment,
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
                    CourseId = c.CourseId,
                    SemesterId = c.SemesterId,
                    Enrollment = c.Enrollment,
                    Instructor = c.Instructor
                }; ;
            }
        }
    }
}