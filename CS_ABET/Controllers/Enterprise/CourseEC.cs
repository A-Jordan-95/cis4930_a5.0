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
    public class CourseEC
    {
        public async Task<IEnumerable<CourseDto>> Get(int id)
        {
            var results = new List<CourseDto>();
            using (var db = new AbetContext())
            {
                try
                {
                    results = await db.Courses.Where(c => c.Id == id)
                    .Select(s =>
                    new CourseDto
                    {
                        Id = s.Id,
                        CourseCode = s.CourseCode,
                        CourseName = s.CourseName
                    }).ToListAsync();
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        public async Task<IEnumerable<CourseDto>> Get()
        {
            var results = new List<CourseDto>();
            using (var db = new AbetContext())
            {
                try
                {
                    results = await db.Courses.Select(s =>
                    new CourseDto
                    {
                        Id = s.Id,
                        CourseCode = s.CourseCode,
                        CourseName = s.CourseName
                    }).ToListAsync();
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        public async Task<CourseDto> AddOrUpdate(CourseDto courseDto)
        {
            using (var db = new AbetContext())
            {
                db.Courses.AddOrUpdate(
                        new Course
                        {
                            Id = courseDto.Id,
                            CourseCode = courseDto.CourseCode,
                            CourseName = courseDto.CourseName
                        });
                await db.SaveChangesAsync();
            }


            return courseDto;
        }

        public CourseDto Remove(CourseDto courseDto)
        {
            using (var db = new AbetContext())
            {
                var result = db.Courses.Remove(new Course
                {
                    Id = courseDto.Id,
                    CourseName = courseDto.CourseName,
                    CourseCode = courseDto.CourseCode
                });
                db.SaveChanges();
                return new CourseDto
                {
                    Id = result.Id,
                    CourseCode = result.CourseCode,
                    CourseName = result.CourseName
                };
            }
        }
    }
}