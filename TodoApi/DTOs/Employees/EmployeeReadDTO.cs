namespace TodoApi.DTOs.Employees
{
    public record EmployeeReadDTO(int EmployeeId, string? FullName, string? Department, DateTime HireDate)
    {
    }
}
