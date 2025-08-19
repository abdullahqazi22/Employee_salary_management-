    using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.DTOs.Employees;
using TodoApi.Interface;

namespace TodoApi.Endpoints.Employee
{
    public static class EmployeeEndPoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/employees", async (IEmployeeService employeeService) =>
            {
                var employees= await employeeService.GetAllEmployeesAsync();
                return Results.Ok(employees);
            });
            app.MapGet("/employees/{id}", async (int id, IEmployeeService employeeService) =>
            {
                var employee = await employeeService.GetEmployeeByIdAsync(id);
                return Results.Ok(employee);
            });
            app.MapPost("/add/employees", async (CreateEmployeeDTO employeeDto, IEmployeeService employeeService) =>
            {
                await employeeService.CreateEmployeeAsync(employeeDto);
                return Results.Created($"/employees/{employeeDto.FullName}", employeeDto);
            });
            app.MapPut("/employees/by-employee/{employeeid}", async (int employeeid, UpdateEmployeeDTO employeeDto, IEmployeeService employeeService) =>
            {
                await employeeService.UpdateEmployeeAsync(employeeid, employeeDto);
                return Results.Ok(new { message = "Employee updated successfully" });
            });
            app.MapDelete("/employees/{employeeid}", async (int employeeid,IEmployeeService employeeService ) =>
            {
                return await employeeService.DeleteEmployeeAsync(employeeid)
                    .ContinueWith(task => 
                    {
                        if (task.IsCompletedSuccessfully)
                        {
                            return Results.Ok(new { message = "Employee deleted successfully" });
                        }
                        else
                        {
                            return Results.NotFound(new { message = $"Employee with ID {employeeid} not found." });
                        }
                    });
                });
        }
    }
}
