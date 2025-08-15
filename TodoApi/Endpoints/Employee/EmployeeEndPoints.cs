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
            app.MapPost("/add/employees", async (CreateEmployeeDTO employeeDto, TodoDB db) =>
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
            app.MapPut("/employees/by-employee/{employeeid}", async (int employeeid, UpdateEmployeeDTO employeeDto, TodoDB db) =>
            {
                var employee = await db.Employees
                .Where(s => s.EmployeeId == employeeid)
                .FirstOrDefaultAsync();
                if (employee == null) return Results.NotFound();
                employee.FullName = employeeDto.FullName;
                employee.Department = employeeDto.Department;
                employee.HireDate = employeeDto.HireDate;
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
                //return Results.NoContent();
                return Results.Ok(employee);
            });
            app.MapDelete("/employees/{employeeid}", async (int employeeid, TodoDB db) =>
            {
                var employee = await db.Employees.Where(s => s.EmployeeId == employeeid).FirstOrDefaultAsync();
                if (employee == null) return Results.NotFound();
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
