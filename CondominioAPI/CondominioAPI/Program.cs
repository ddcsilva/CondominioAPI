using CondominioAPI.Domain.Repositories;
using CondominioAPI.Application.Services;
using CondominioAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Adicionar servi�os ao container.
builder.Services.AddControllers();

// Adicionar FluentValidation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

// Registrar reposit�rios e servi�os
builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
builder.Services.AddScoped<ICondominioService, CondominioService>();

// Configurar o Swagger para gerar a documenta��o da API
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "CondominioAPI", Version = "v1" }); });

var app = builder.Build();

// Configurar o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Ativar o middleware do Swagger para gerar o arquivo JSON da documenta��o
app.UseSwagger();

// Configurar o endpoint do Swagger UI para exibir a documenta��o interativa da API
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CondominioAPI v1"); });

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
