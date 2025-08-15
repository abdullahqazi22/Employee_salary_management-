namespace TodoApi.DTOs.Salary
{
    public record UpdateSalaryDTO(int EmployeeId,int Amount, DateTime MonthYear)
    {
    }
}
