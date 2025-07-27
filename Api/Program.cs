using Carter;
using Microsoft.EntityFrameworkCore;
using Api;
using Api.Calculations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ICalculationsService, CalculationService>();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

// Migrate the database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
