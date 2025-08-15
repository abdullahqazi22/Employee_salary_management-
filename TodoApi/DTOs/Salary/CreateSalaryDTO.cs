namespace TodoApi.DTOs.Salary
{
    public record CreateSalaryDTO(int EmployeeId,int Amount, DateTime MonthYear)
    {
    }
}
