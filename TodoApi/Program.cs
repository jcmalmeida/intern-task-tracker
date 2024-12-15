using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Endpoints;
using TodoApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=127.0.0.1;Port=3306;Database=todo;User=intern;Password=intern123;";
builder.Services.AddDbContext<TodoDb>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUIConfig();
}

app.MapTodoEndpoints();

app.Run();
