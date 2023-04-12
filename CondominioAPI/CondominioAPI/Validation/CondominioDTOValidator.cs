using CondominioAPI.Application.DTOs;
using FluentValidation;

namespace CondominioAPI.Validation
{
    public class CondominioDTOValidator : AbstractValidator<CondominioDTO>
    {
        public CondominioDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do condomínio é obrigatório.")
                .MaximumLength(200).WithMessage("O nome do condomínio não pode ter mais de 200 caracteres.");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("O CNPJ do condomínio é obrigatório.")
                .Length(14).WithMessage("O CNPJ do condomínio deve ter exatamente 14 caracteres.");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O endereço do condomínio é obrigatório.")
                .MaximumLength(300).WithMessage("O endereço do condomínio não pode ter mais de 300 caracteres.");

            RuleFor(x => x.NumeroUnidades)
                .GreaterThan(0).WithMessage("O número de unidades deve ser maior que 0.");

            RuleFor(x => x.NumeroBlocos)
                .GreaterThan(0).When(x => x.NumeroBlocos.HasValue).WithMessage("O número de blocos deve ser maior que 0, se informado.");

            RuleFor(x => x.DataFundacao)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("A data de fundação não pode ser futura.");
        }
    }
}
