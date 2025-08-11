using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoApi.Entity;

namespace TodoApi.Data
{
    public class TodoDB : DbContext
    {
        public TodoDB(DbContextOptions<TodoDB> options) : base(options)
        {
        }
        public DbSet<Entity.Employee> Employees { get; set; }
        public DbSet<Entity.Salary> Salaries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>()
                .HasKey(s => s.SalaryId);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            // One Salary → Many Employees
            modelBuilder.Entity<Salary>()
                .HasMany(s => s.Employees)
                .WithOne(e => e.Salary)
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.Restrict); // prevent deleting salary if employees exist
        }
    }
}