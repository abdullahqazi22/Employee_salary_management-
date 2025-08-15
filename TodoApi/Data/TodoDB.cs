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
            //Employee --> salary (One to many) relationship
            modelBuilder.Entity<Entity.Employee>()
                .HasKey(e => e.EmployeeId); // Primary key for Employee

                modelBuilder.Entity<Entity.Salary>()
                .HasKey(s => s.SalaryId); // Primary key for Salary

            modelBuilder.Entity<Entity.Employee>()
                .HasMany(e => e.Salaries)
                .WithOne(s =>s.Employee)
                .HasForeignKey(s => s.EmployeeId) // Foreign key relationship
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete behavior
        }
    }
}