using CondominioAPI.Domain.Repositories;
using CondominioAPI.Domain.Services;
using CondominioAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao container.
builder.Services.AddControllers();

// Adicionar FluentValidation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

// Registrar reposit�rios e servi�os
builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
builder.Services.AddScoped<ICondominioService, CondominioService>();

var app = builder.Build();

// Configurar o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
