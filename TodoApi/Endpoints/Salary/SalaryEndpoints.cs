using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs.Salary;
using TodoApi.Interface;

namespace TodoApi.Endpoints.Salary
{
    public static class SalaryEndpoints
    {
        public static void MapSalaryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/salaries", async (ISalaryService salaryService) =>
            {
              var salaries = await salaryService.GetAllSalariesAsync();
                return Results.Ok(salaries);
            });
       
            app.MapGet("/salaries/{employeeid}", async (int employeeid,ISalaryService salaryService) =>
            {
                var salary = await salaryService.GetSalaryByEmployeeIdAsync(employeeid);
                if (salary == null) return Results.NotFound();
                return Results.Ok(salary);
            });

            app.MapPost("/salaries/add-employee-salary/{employeeid}", async (DTOs.Salary.CreateSalaryDTO salaryDto, ISalaryService salaryService) =>
            {
                await salaryService.CreateSalaryAsync(salaryDto);
                return Results.Created($"/salaries/{salaryDto.EmployeeId}", salaryDto);
            });
            app.MapPut("/salaries/by-employee/{employeeid}", async (int employeeid,UpdateSalaryDTO updateSalaryDTO ,ISalaryService salaryService) =>
            {
                await salaryService.UpdateSalaryAsync(employeeid, updateSalaryDTO);
                return Results.Ok(new { message = "Salary updated successfully" });
            });
            app.MapDelete("/salaries/by-employee/{employeeid}", async (int employeeid, ISalaryService salaryService) =>
            {
                await salaryService.DeleteSalaryAsync(employeeid)
                .ContinueWith(task => 
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        return Results.Ok(new { message = "Salary deleted successfully" });
                    }
                    else
                    {
                        return Results.NotFound(new { message = $"Salary for employee with ID {employeeid} not found." });
                    }
                });
              });
        }
    }
}
