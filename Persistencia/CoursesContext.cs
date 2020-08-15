using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class CoursesContext : DbContext
    {

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseTeacher> CourseTeacher { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Price> Price { get; set; }

        public CoursesContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTeacher>().HasKey(pk => new { pk.TeacherId, pk.CourseId });
        }
    }
}
