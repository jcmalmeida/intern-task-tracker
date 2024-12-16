using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Endpoints;
using TodoApi.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5258);
});

var connectionString = "Server=mysql;Port=3306;Database=todo;User=intern;Password=intern123;";
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
