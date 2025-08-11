namespace TodoApi.DTOs.Employees
{
    public record CreateEmployeeDTO(string? FullName, string? Department, DateTime HireDate)
    {
    }
}
