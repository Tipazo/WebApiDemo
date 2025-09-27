using Microsoft.Data.SqlClient;
using WebApiDemo.Controllers;
using WebApiDemo.Services.SEmployee;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployee, EmployeeService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

app.MapGet("/", () => Results.Text("API is running !!")).ExcludeFromDescription();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();;

app.Run();
