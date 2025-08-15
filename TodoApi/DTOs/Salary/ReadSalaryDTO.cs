namespace TodoApi.DTOs.Salary
{
    public record ReadSalaryDTO(int EmployeeId, DateTime MonthYear, int Amount)
    {
    }
}
