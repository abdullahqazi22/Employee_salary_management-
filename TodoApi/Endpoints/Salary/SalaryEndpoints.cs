using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace TodoApi.Endpoints.Salary
{
    public static class SalaryEndpoints
    {
        public static void MapSalaryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/salaries", async (TodoDB db) =>
            {
                    var employee= await db.Salaries
                    .Select(s => new DTOs.Salary.ReadSalaryDTO(s.EmployeeId,s.MonthYear, s.Amount))
                    .ToListAsync();
                return Results.Ok(employee);
            });
       
            app.MapGet("/salaries/{employeeid}", async (int employeeid, TodoDB db) =>
            {
                var salary = await db.Salaries
                 .Include(s => s.Employee) // Load the related Employee
                 .FirstOrDefaultAsync(s => s.EmployeeId == employeeid);
                if (salary == null) return Results.NotFound();

                var dto = new DTOs.Salary.ReadSalaryDTO(
                    salary.EmployeeId,
                    salary.MonthYear,
                    salary.Amount
                 );

                return Results.Ok(dto);

            });

            app.MapPost("/salaries/{employeeid}", async (DTOs.Salary.CreateSalaryDTO salaryDto, TodoDB db) =>
            {
                var salary = new Entity.Salary
                {
                    EmployeeId = salaryDto.EmployeeId,
                    Amount = salaryDto.Amount,
                    MonthYear = salaryDto.MonthYear,
                };
                db.Salaries.Add(salary);
                await db.SaveChangesAsync();
                return Results.Created($"/salaries/{salary.EmployeeId}", new DTOs.Salary.ReadSalaryDTO(salary.EmployeeId, salary.MonthYear, salary.Amount));
            });
            app.MapPut("/salaries/by-employee/{employeeid}", async (int employeeid, DTOs.Salary.UpdateSalaryDTO salaryDto, TodoDB db) =>
            {
                var salary = await db.Salaries
                .Where(s => s.EmployeeId == employeeid)
                .FirstOrDefaultAsync();
                if (salary == null) return Results.NotFound();
                salary.Amount = salaryDto.Amount;
                salary.MonthYear = salaryDto.MonthYear;
                db.Salaries.Update(salary);
                await db.SaveChangesAsync();
                //return Results.NoContent();
                return Results.Ok(new { message = "Salary updated successfully" });

            });
            app.MapDelete("/salaries/by-employee/{employeeid}", async (int employeeid, TodoDB db) =>
            {
                var salary = await db.Salaries.Where(s=> s.EmployeeId == employeeid).FirstOrDefaultAsync();
                if (salary == null) return Results.NotFound();
                db.Salaries.Remove(salary);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
