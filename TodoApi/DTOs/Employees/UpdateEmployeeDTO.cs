namespace TodoApi.DTOs.Employees
{
    public record UpdateEmployeeDTO(string? FullName, string? Department, DateTime HireDate)
    {
    }
}
