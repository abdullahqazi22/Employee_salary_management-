//using System.ComponentModel.DataAnnotations;

//namespace TodoApi.Entity
//{
//    public class Employee
//    {
//        [Key]
//        public int EmployeeId { get; set; }
//        public string? FullName { get; set; }
//        public string? Department { get; set; }
//        public DateTime HireDate { get; set; }
//        public ICollection<Salary> Salaries { get; set; } 
//    }

//    public class Salary
//    {
//            [Key]
//            public int SalaryId { get; set; }   
//            public int EmployeeId { get; set; } // FK to Employee
//            public string? MonthYear { get; set; }
//            public int Amount { get; set; }
//            public Employee? Employee { get; set; } //navigation 
//    }


//}
using System.Collections.Generic;

namespace TodoApi.Entity
{
    public class Employee
    {
        // Primary key
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }

        // Foreign key to Salary
        public int SalaryId { get; set; }
        public Salary Salary { get; set; }
    }

    public class Salary
    {
        // Primary key
        public int SalaryId { get; set; }
        public int Amount { get; set; }
        public string? MonthYear { get; set; }

        // Navigation property for all employees with this salary
        public ICollection<Employee> Employees { get; set; }
    }
}

