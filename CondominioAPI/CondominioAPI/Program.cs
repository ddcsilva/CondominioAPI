using CondominioAPI.Infrastructure.Repositories;
using CondominioAPI.Application.Services;
using CondominioAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using CondominioAPI.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Adicionar serviços ao container.
builder.Services.AddControllers();

// Adicionar FluentValidation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

builder.Services.AddAutoMapper(typeof(CondominioProfile));

// Registrar repositórios e serviços
builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
builder.Services.AddScoped<ICondominioService, CondominioService>();

// Configurar o Swagger para gerar a documentação da API
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "CondominioAPI", Version = "v1" }); });

var app = builder.Build();

// Configurar o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Ativar o middleware do Swagger para gerar o arquivo JSON da documentação
app.UseSwagger();

// Configurar o endpoint do Swagger UI para exibir a documentação interativa da API
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CondominioAPI v1"); });

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
