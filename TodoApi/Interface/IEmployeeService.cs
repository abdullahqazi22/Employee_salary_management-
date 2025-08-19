namespace TodoApi.Interface
{
    public interface IEmployeeService
    {
        // Define methods for employee management
        Task<IEnumerable<DTOs.Employees.EmployeeReadDTO>> GetAllEmployeesAsync();
        Task<DTOs.Employees.EmployeeReadDTO> GetEmployeeByIdAsync(int employeeId);
        Task<DTOs.Employees.EmployeeReadDTO> CreateEmployeeAsync(DTOs.Employees.CreateEmployeeDTO employeeDto);
        Task<DTOs.Employees.EmployeeReadDTO> UpdateEmployeeAsync(int employeeId, DTOs.Employees.UpdateEmployeeDTO employeeDto);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
