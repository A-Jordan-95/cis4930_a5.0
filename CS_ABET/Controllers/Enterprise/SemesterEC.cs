using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;

namespace CS_ABET.Controllers.Enterprise
{
    public class SemesterEC : BaseEC
    {
        public IEnumerable<SemesterDto> Get()
        {
            var result = new List<SemesterDto>();
            using(var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var sql = "select * from Semester order by OrderNumber";
                    var cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new SemesterDto { Id = (int)reader["Id"], Name = reader["Name"].ToString() });
                    }
                    
                } catch(Exception e)
                {

                }
            }
            return result;
        }

        public SemesterDto GetById(int id)
        {
            var result = new SemesterDto();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var sql = $"select * from Semester where Id = {id}";
                    var cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new SemesterDto { Id = (int)reader["Id"], Name = reader["Name"].ToString() };
                    }

                }
                catch (Exception e)
                {

                }
            }
            return result;
        }

        //public IEnumerable<Semester> Search(string query)
        //{
        //    return DbMock.Semesters.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).OrderBy(s => s.Name);
        //}

        public IEnumerable<SemesterDto> Search(string query)
        {
            var result = new List<SemesterDto>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var sql = $"select * from Semester where Name like '%{query}%' order by LastModified desc";
                    var cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new SemesterDto { Id = (int)reader["Id"], Name = reader["Name"].ToString() });
                    }

                }
                catch (Exception e)
                {

                }
            }
            return result;
        }


        public Semester AddOrUpdate(Semester semester)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql;
                if (semester.Id > 0)
                {
                    sql = $"SemesterUpdate";
                    var cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Name", semester.Name));
                    cmd.Parameters.Add(new SqlParameter("Id", semester.Id));
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    } catch(Exception e)
                    {

                    }
                    connection.Close();
                }
                else
                {
                    sql = $"SemesterInsert";
                    var cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Name", semester.Name));
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    } catch(Exception e)
                    {

                    }

                    connection.Close();
                }
            }


            return semester;
        }

        public bool Remove(Semester semester)
        {
            return Remove(semester.Id);
        }

        public bool Remove(int id)
        {
            var result = false;
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var sql = $"delete Semester where id = {id}";
                    var cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    var reader = cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {

                }
            }
            return result;
        }
    }
}