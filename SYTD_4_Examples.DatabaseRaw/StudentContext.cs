using Microsoft.EntityFrameworkCore;

namespace SYTD_4_Examples.DatabaseRaw
{
    internal class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentContext(DbContextOptions options) : base(options)
        {

        }
    }
}