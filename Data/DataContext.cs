using Microsoft.EntityFrameworkCore;
using student_management.Model;

namespace student_management.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Data Structure", Description = "Algorizm", CourseCode = "202", CreditHours = 4, Hours = 14,  TargetGroup = "abc", DepartmentId = 1 },
                new Course { Id = 2, Title = "English", Description = "Acd", CourseCode = "203", CreditHours = 4, Hours = 14,  TargetGroup = "abc", DepartmentId = 2 },
                new Course { Id = 3, Title = "Maths", Description = "Acd", CourseCode = "204", CreditHours = 4, Hours = 14,  TargetGroup = "abc", DepartmentId = 3 }
            );
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, UserName = "xyz", BlockNumber = "104", DepartmentName = "Electrical Enginering", YearOfEstablishment = 1999 },
                new Department { Id = 2, UserName = "abcd", BlockNumber = "105", DepartmentName = "Computer Enginering", YearOfEstablishment = 1999 },
                new Department { Id = 3, UserName = "hijk", BlockNumber = "106", DepartmentName = "Agricalture", YearOfEstablishment = 1999 }
            );
            // modelBuilder.Entity<Course>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Course>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Department>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Department>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Visitor>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Visitor>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Student>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Student>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Admin>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Admin>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Teacher>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
            // modelBuilder.Entity<Teacher>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate(); base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }


}