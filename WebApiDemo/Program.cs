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

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

//app.MapGet("/", () => Results.Text("API is running 🚀"));

app.MapGet("/db-check", async () =>
{
    var connStr = Environment.GetEnvironmentVariable("STRING_CONNECT");
    using var conn = new SqlConnection(connStr);
    try
    {
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT GETDATE()";
        var result = await cmd.ExecuteScalarAsync();
        return Results.Ok($"Conexión exitosa: {result}");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error de conexión: {ex.Message}");
    }
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();;

app.Run();
