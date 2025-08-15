namespace TodoApi.DTOs.Employees
{
    public record UpdateEmployeeDTO(int EmployeeId ,string? FullName, string? Department, DateTime HireDate)
    {
    }
}
