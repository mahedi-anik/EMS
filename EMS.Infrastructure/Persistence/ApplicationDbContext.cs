using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

        // Override the OnModelCreating method to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-one relationship between Employee and Department
            modelBuilder.Entity<Department>()
               .HasOne(d => d.Manager)
               .WithOne(e => e.Department)
               .HasForeignKey<Department>(d => d.ManagerId)
               .OnDelete(DeleteBehavior.Cascade);

            // Seed Departments
            var departmentId1 = Guid.NewGuid().ToString();
            var departmentId2 = Guid.NewGuid().ToString();

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = departmentId1, DepartmentName = "HR", ManagerId = departmentId1, Budget = 50000, CreateBy = "admin", CreateDate = DateTime.UtcNow, UpdateBy = "admin", IsActive = true, IsDelete = false },
                new Department { Id = departmentId2, DepartmentName = "IT", ManagerId = departmentId2, Budget = 100000, CreateBy = "admin", CreateDate = DateTime.UtcNow, UpdateBy = "admin", IsActive = true, IsDelete = false }
            );

            // Seed Employees
            var employeeId1 = Guid.NewGuid().ToString();
            var employeeId2 = Guid.NewGuid().ToString();

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = employeeId1, EmployeeName = "John", Position = "Manager", DepartmentId = departmentId1, CreateDate = DateTime.UtcNow, UpdateBy = "admin", IsActive = true, IsDelete = false },
                new Employee { Id = employeeId2, EmployeeName = "Jane", Position = "Developer", DepartmentId = departmentId2, CreateDate = DateTime.UtcNow, UpdateBy = "admin", IsActive = true, IsDelete = false }
            );

            // Seed PerformanceReviews
            modelBuilder.Entity<PerformanceReview>().HasData(
                new PerformanceReview { Id = Guid.NewGuid().ToString(), EmployeeId = employeeId1, ReviewDate = DateTime.UtcNow, ReviewScore = 4.5m, ReviewNote = "Excellent performance", CreateBy = "admin", CreateDate = DateTime.UtcNow, IsActive = true, IsDelete = false },
                new PerformanceReview { Id = Guid.NewGuid().ToString(), EmployeeId = employeeId2, ReviewDate = DateTime.UtcNow, ReviewScore = 3.8m, ReviewNote = "Good performance", CreateBy = "admin", CreateDate = DateTime.UtcNow, IsActive = true, IsDelete = false }
            );
        }

    }
}
