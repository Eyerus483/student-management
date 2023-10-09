using Microsoft.EntityFrameworkCore;
using student_management.Model;

namespace student_management.Data
{
    public class DataContext : DbContext
    {
     public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; } 
        public DbSet<Course> Courses { get; set; } 
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Admin> Admins { get; set; }
    }
}