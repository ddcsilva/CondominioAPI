using CondominioAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<CondominioDTOValidator>();

        return services;
    }
}
