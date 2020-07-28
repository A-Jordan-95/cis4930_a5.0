using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Data.Entity;
using WebGrease.Css.Extensions;

namespace CS_ABET.Controllers.Enterprise
{
    public class SemesterEC : BaseEC
    {
        public async Task<IEnumerable<SemesterDto>> Get()
        {
            var results = new List<SemesterDto>();
            using(var db = new AbetContext())
            {
                try
                {
                    results = await db.Semesters.Select(s => new SemesterDto { Id = s.Id, Name = s.Name }).ToListAsync();
                } catch(Exception e)
                {

                }
            }

            return results;
        }

        public async Task<SemesterDto> GetById(int id)
        {
            using (var db = new AbetContext())
            {
                return await db.Semesters.Where(s => s.Id == id)?.Take(1).Select(s => new SemesterDto { Id = s.Id, Name = s.Name }).FirstOrDefaultAsync();
            }

        }

        public async Task<IEnumerable<SemesterDto>> Search(string query)
        {

            using (var db = new AbetContext())
            {
                return await db.Semesters
                    .Where(s => s.Name.ToUpper().Contains(query.ToUpper()))
                    .Select(s => new SemesterDto { Id = s.Id, Name = s.Name })
                    .ToListAsync();
            }

        }


        public async Task <SemesterDto> AddOrUpdate(SemesterDto semesterDto)
        {
            var newSemester = new Semester { Id = semesterDto.Id, Name = semesterDto.Name, LastModified = DateTime.Now };
            using (var db = new AbetContext())
            {
                try
                {
                    
                    db.Semesters.AddOrUpdate(newSemester);
                    await db.SaveChangesAsync();
                } catch(Exception e)
                {

                }
            }


            return new SemesterDto { Id = newSemester.Id, Name = newSemester.Name };
        }

        public SemesterDto Remove(SemesterDto semesterDto)
        {
            using (var db = new AbetContext())
            {
                var result = db.Semesters.Remove(new Semester { Id = semesterDto.Id, Name = semesterDto.Name });
                db.SaveChanges();
                return new SemesterDto { Id = result.Id, Name = result.Name };
            }
        }

        public async Task<Semester> RemoveById(int id)
        {
            using (var db = new AbetContext())
            {
                var toRemove = db.Semesters.FirstOrDefault(s => s.Id == id);
                db.Semesters.Remove(toRemove);
                await db.SaveChangesAsync();
                return toRemove;
            }
        }

    }
}