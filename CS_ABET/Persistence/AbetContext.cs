using CS_ABET.Persistence.Models;
using System.Data.Entity;

namespace CS_ABET.Persistence
{
    public class AbetContext : DbContext
    {
        public AbetContext() : base("Server=.;Database=ABET_DB;Trusted_Connection=True;")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Class> Classes { get; set; }
    }
}