namespace TodoApi.Interface
{
    public interface ISalaryService
    {
        // Define methods for salary management
        Task<IEnumerable<DTOs.Salary.ReadSalaryDTO>> GetAllSalariesAsync();
        Task<DTOs.Salary.ReadSalaryDTO> GetSalaryByEmployeeIdAsync(int employeeId);
        Task<DTOs.Salary.ReadSalaryDTO> CreateSalaryAsync(DTOs.Salary.CreateSalaryDTO salaryDto);
        Task<DTOs.Salary.ReadSalaryDTO> UpdateSalaryAsync(int employeeId, DTOs.Salary.UpdateSalaryDTO salaryDto);
        Task DeleteSalaryAsync(int employeeId);
    }
}
