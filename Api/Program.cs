
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCarter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();
app.Run();
