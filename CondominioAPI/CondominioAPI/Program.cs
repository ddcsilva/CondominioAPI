using CondominioAPI.Domain.Repositories;
using CondominioAPI.Domain.Services;
using CondominioAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container.
builder.Services.AddControllers();

// Adicionar FluentValidation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

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
