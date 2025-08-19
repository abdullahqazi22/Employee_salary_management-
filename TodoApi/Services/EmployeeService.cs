using TodoApi.Data;
using TodoApi.DTOs.Employees;

namespace TodoApi.Services
{
    public class EmployeeService : Interface.IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly TodoDB _todoDB;
        public EmployeeService(ILogger<EmployeeService> logger, TodoDB todoDB)
        {
            _logger = logger;
            _todoDB = todoDB;
        }
        public async Task<EmployeeReadDTO> CreateEmployeeAsync(CreateEmployeeDTO employeeDto)
        {
            var employee = new Entity.Employee
            {
                FullName = employeeDto.FullName,
                Department = employeeDto.Department,
                HireDate = employeeDto.HireDate
            };
            _todoDB.Employees.Add(employee);
            await _todoDB.SaveChangesAsync();
            return new EmployeeReadDTO(employee.EmployeeId, employee.FullName, employee.Department, employee.HireDate);
        }

        public Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = _todoDB.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }
            _todoDB.Employees.Remove(employee);
            return _todoDB.SaveChangesAsync();
        }

        public Task<IEnumerable<EmployeeReadDTO>> GetAllEmployeesAsync()
        {
            var employees = _todoDB.Employees.Select(e => new EmployeeReadDTO(e.EmployeeId, e.FullName, e.Department, e.HireDate));
            return Task.FromResult<IEnumerable<EmployeeReadDTO>>(employees.ToList());
        }

        public Task<EmployeeReadDTO> GetEmployeeByIdAsync(int employeeId)
        {
           var employee = _todoDB.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }
            return Task.FromResult(new EmployeeReadDTO(employee.EmployeeId, employee.FullName, employee.Department, employee.HireDate));
        }

        public Task<EmployeeReadDTO> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO employeeDto)
        {
            var employee = _todoDB.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }
            employee.FullName=employeeDto.FullName;
            employee.Department = employeeDto.Department;
            employee.HireDate = employeeDto.HireDate;
            _todoDB.Employees.Update(employee);
            return _todoDB.SaveChangesAsync().ContinueWith(t => 
                new EmployeeReadDTO(employee.EmployeeId, employee.FullName, employee.Department, employee.HireDate));
        }
    }
}
