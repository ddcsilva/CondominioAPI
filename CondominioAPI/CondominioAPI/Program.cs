using CondominioAPI.Extensions;
using CondominioAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CondominioAPI.Application.Mappings;
using CondominioAPI.Validation;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Serilog
builder.Services.AddSerilogConfiguration(builder.Configuration);

builder.Host.UseSerilog();

// Adicionar servi�os ao container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar FluentValidation
builder.Services.AddFluentValidationConfiguration();

builder.Services.AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

builder.Services.AddAutoMapper(typeof(CondominioProfile));

// Registrar reposit�rios e servi�os
builder.Services.RegisterRepositoriesAndServices();

// Configurar o Swagger para gerar a documenta��o da API
builder.Services.AddSwaggerConfiguration();

// Configurar CORS
builder.Services.AddCustomCors();

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

app.UseCustomCors(); // Adicione esta linha para habilitar o CORS

app.MapControllers();

app.Run();

public partial class Program { }

