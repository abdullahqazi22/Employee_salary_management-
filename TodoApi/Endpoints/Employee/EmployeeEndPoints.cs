using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.DTOs.Employees;

namespace TodoApi.Endpoints.Employee
{
    public static class EmployeeEndPoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/employees", async (TodoDB db) =>
            {
                return await db.Employees
                    .Select(e => new EmployeeReadDTO(e.EmployeeId, e.FullName, e.Department, e.HireDate))
                    .ToListAsync();
            });
            app.MapGet("/employees/{id}", async (int id, TodoDB db) =>
            {
                var employee = await db.Employees.FindAsync(id);
                if (employee == null) return Results.NotFound();

                var dto = new EmployeeReadDTO(employee.EmployeeId, employee.FullName, employee.Department, employee.HireDate);
                return Results.Ok(dto);
            });
            app.MapPost("/employees", async (CreateEmployeeDTO employeeDto, TodoDB db) =>
            {
                var employee = new Entity.Employee
                {
                    FullName = employeeDto.FullName,
                    Department = employeeDto.Department,
                    HireDate = employeeDto.HireDate
                };
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return Results.Created($"/employees/{employee.EmployeeId}", new EmployeeReadDTO(employee.EmployeeId, employee.FullName, employee.Department, employee.HireDate));
            });
            app.MapPut("/employees/{id}", async (int id, UpdateEmployeeDTO employeeDto, TodoDB db) =>
            {
                var employee = await db.Employees.FindAsync(id);
                if (employee == null) return Results.NotFound();
                employee.FullName = employeeDto.FullName;
                employee.Department = employeeDto.Department;
                employee.HireDate = employeeDto.HireDate;
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/employees/{id}", async (int id, TodoDB db) =>
            {
                var employee = await db.Employees.FindAsync(id);
                if (employee == null) return Results.NotFound();
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
