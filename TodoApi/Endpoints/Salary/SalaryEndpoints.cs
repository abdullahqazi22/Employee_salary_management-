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
                return await db.Salaries
                    .Select(s => new DTOs.Salary.ReadSalaryDTO(s.SalaryId,s.MonthYear, s.Amount))
                    .ToListAsync();
            });
       
            app.MapGet("/salaries/{id}", async (int id, TodoDB db) =>
            {
                var salary = await db.Salaries.FindAsync(id);
                if (salary == null) return Results.NotFound();
                var dto = new DTOs.Salary.ReadSalaryDTO(salary.SalaryId, salary.MonthYear, salary.Amount);
                return Results.Ok(dto);
            });

            app.MapPost("/salaries", async (DTOs.Salary.CreateSalaryDTO salaryDto, TodoDB db) =>
            {
                var salary = new Entity.Salary
                {
                    Amount = salaryDto.Amount,
                    MonthYear = salaryDto.MonthYear,
                };
                db.Salaries.Add(salary);
                await db.SaveChangesAsync();
                return Results.Created($"/salaries/{salary.SalaryId}", new DTOs.Salary.ReadSalaryDTO(salary.SalaryId, salary.MonthYear, salary.Amount));
            });
            app.MapPut("/salaries/{id}", async (int id, DTOs.Salary.UpdateSalaryDTO salaryDto, TodoDB db) =>
            {
                var salary = await db.Salaries.FindAsync(id);
                if (salary == null) return Results.NotFound();
                salary.Amount = salaryDto.Amount;
                db.Salaries.Update(salary);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/salaries/{id}", async (int id, TodoDB db) =>
            {
                var salary = await db.Salaries.FindAsync(id);
                if (salary == null) return Results.NotFound();
                db.Salaries.Remove(salary);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
