using Microsoft.EntityFrameworkCore;

namespace OnlineCourses
{
    public class AppContext : DbContext
    {

        private const string connectionString =
            @"Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=CursosOnline;Integrated Security=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Course> Course { get; set; }
    }
}
