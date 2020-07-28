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
    public class SubjectEC : BaseEC
    {
        public async Task<IEnumerable<SubjectDto>> Get()
        {
            var results = new List<SubjectDto>();
            using(var db = new AbetContext())
            {
                try
                {
                    results = await db.Subjects.Select(s => new SubjectDto { Id = s.Id, Name = s.Name }).ToListAsync();
                } catch(Exception e)
                {

                }
            }

            return results;
        }

        public async Task<SubjectDto> GetById(int id)
        {
            using (var db = new AbetContext())
            {
                return await db.Subjects.Where(s => s.Id == id)?.Take(1).Select(s => new SubjectDto { Id = s.Id, Name = s.Name }).FirstOrDefaultAsync();
            }

        }

        public async Task<IEnumerable<SubjectDto>> Search(string query)
        {

            using (var db = new AbetContext())
            {
                return await db.Subjects
                    .Where(s => s.Name.ToUpper().Contains(query.ToUpper()))
                    .Select(s => new SubjectDto { Id = s.Id, Name = s.Name })
                    .ToListAsync();
            }

        }


        public async Task <SubjectDto> AddOrUpdate(SubjectDto SubjectDto)
        {
            var newSubject = new Subject { Id = SubjectDto.Id, Name = SubjectDto.Name, LastModified = DateTime.Now };
            using (var db = new AbetContext())
            {
                try
                {
                    
                    db.Subjects.AddOrUpdate(newSubject);
                    await db.SaveChangesAsync();
                } catch(Exception e)
                {

                }
            }


            return new SubjectDto { Id = newSubject.Id, Name = newSubject.Name };
        }

        public SubjectDto Remove(SubjectDto SubjectDto)
        {
            using (var db = new AbetContext())
            {
                var result = db.Subjects.Remove(new Subject { Id = SubjectDto.Id, Name = SubjectDto.Name });
                db.SaveChanges();
                return new SubjectDto { Id = result.Id, Name = result.Name };
            }
        }

        public async Task<Subject> RemoveById(int id)
        {
            using (var db = new AbetContext())
            {
                var toRemove = db.Subjects.FirstOrDefault(s => s.Id == id);
                db.Subjects.Remove(toRemove);
                await db.SaveChangesAsync();
                return toRemove;
            }
        }

    }
}