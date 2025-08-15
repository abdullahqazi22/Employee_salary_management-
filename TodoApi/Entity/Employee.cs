using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Entity
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }// Primary key
        public string? FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }
        public string? Department { get; set; }
        public ICollection<Salary>? Salaries { get; set; } = new List<Salary>();
    }
    public class Salary
    {
        public int EmployeeId { get; set; }// Foreign key to Employee
        [Column(TypeName = "date")]
        public DateTime MonthYear { get; set; }
        [Key]
        public int SalaryId { get; set; } // Primary key
        public int Amount { get; set; }
        public Employee Employee { get; set; } // Navigation property
    }
}
