using TodoApi.Data;
using TodoApi.DTOs.Salary;

namespace TodoApi.Services
{
    public class SalaryService : Interface.ISalaryService
    {
        private readonly ILogger<SalaryService> _logger;
        private readonly TodoDB _todoDB;
        public SalaryService(ILogger<SalaryService> logger, TodoDB todoDB)
        {
            _logger = logger;
            _todoDB = todoDB;
        }
        public async Task<ReadSalaryDTO> CreateSalaryAsync(CreateSalaryDTO salaryDto)
        {
            var salary = new Entity.Salary
            {
                EmployeeId = salaryDto.EmployeeId,
                Amount = salaryDto.Amount,
                MonthYear = salaryDto.MonthYear
            };
            _todoDB.Salaries.Add(salary);
            _todoDB.SaveChanges();
            return new ReadSalaryDTO(salary.EmployeeId, salary.MonthYear, salary.Amount);
        }

        public Task DeleteSalaryAsync(int employeeId)
        {
            var salary = _todoDB.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId);
            if (salary == null)
            {
                throw new KeyNotFoundException($"Salary for employee with ID {employeeId} not found.");
            }
            _todoDB.Salaries.Remove(salary);
            return _todoDB.SaveChangesAsync();
        }

        public Task<IEnumerable<ReadSalaryDTO>> GetAllSalariesAsync()
        {
            var salary = _todoDB.Salaries
                .Select(s => new ReadSalaryDTO(s.EmployeeId, s.MonthYear, s.Amount));
             return Task.FromResult<IEnumerable<ReadSalaryDTO>>(salary.ToList());
        }

        public Task<ReadSalaryDTO> GetSalaryByEmployeeIdAsync(int employeeId)
        {
            var salary = _todoDB.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId);
            if (salary == null)
            {
                throw new KeyNotFoundException($"Salary for employee with ID {employeeId} not found.");
            }
            return Task.FromResult(new ReadSalaryDTO(salary.EmployeeId, salary.MonthYear, salary.Amount));
        }

        public Task<ReadSalaryDTO> UpdateSalaryAsync(int employeeId, UpdateSalaryDTO salaryDto)
        {
            var salary = _todoDB.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId);
            if (salary == null)
            {
                throw new KeyNotFoundException($"Salary for employee with ID {employeeId} not found.");
            }
            salary.Amount = salaryDto.Amount;
            salary.MonthYear = salaryDto.MonthYear;
            _todoDB.Salaries.Update(salary);
            return _todoDB.SaveChangesAsync().ContinueWith(_ => new ReadSalaryDTO(salary.EmployeeId, salary.MonthYear, salary.Amount));
        }
    }
}
