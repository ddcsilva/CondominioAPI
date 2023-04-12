using CondominioAPI.Domain.Repositories;
using CondominioAPI.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
builder.Services.AddScoped<ICondominioService, CondominioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
