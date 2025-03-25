using Microsoft.EntityFrameworkCore;

namespace Manager_SIMS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Faculty" },
                new Role { RoleId = 3, RoleName = "Student" }
            );
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Faculty)
                .WithMany()
                .HasForeignKey(g => g.FacultyId)
                .OnDelete(DeleteBehavior.NoAction); // Ngăn chặn vòng lặp xoá dữ liệu

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Enrollment)
                .WithMany()
                .HasForeignKey(g => g.EnrollmentId)
                .OnDelete(DeleteBehavior.NoAction); // Cũng áp dụng với Enrollment

            base.OnModelCreating(modelBuilder);
        }

    }
}
