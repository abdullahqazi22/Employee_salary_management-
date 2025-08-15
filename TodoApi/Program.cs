using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Endpoints.Employee;
using TodoApi.Endpoints.Salary;
using Microsoft.OpenApi.Models; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerGen; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive
using Swashbuckle.AspNetCore.Swagger; // Add this using directive

var builder = WebApplication.CreateBuilder(args);


// Allow all CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<TodoDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});


var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
});
app.MapEmployeeEndpoints();
app.MapSalaryEndpoints();

app.Run();
