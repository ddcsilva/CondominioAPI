using CondominioAPI.Application.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

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
                .Length(14).WithMessage("O CNPJ do condomínio deve ter exatamente 14 caracteres.")
                .Must(CnpjValido).WithMessage("O CNPJ do condomínio é inválido.");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O endereço do condomínio é obrigatório.")
                .MaximumLength(300).WithMessage("O endereço do condomínio não pode ter mais de 300 caracteres.")
                .Must(FormatoEnderecoValido).WithMessage("O endereço do condomínio não está no formato correto.");

            RuleFor(x => x.NumeroUnidades)
                .GreaterThan(0).WithMessage("O número de unidades deve ser maior que 0.");

            RuleFor(x => x.NumeroBlocos)
                .GreaterThan(0).When(x => x.NumeroBlocos.HasValue).WithMessage("O número de blocos deve ser maior que 0, se informado.");

            RuleFor(x => x.DataFundacao)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("A data de fundação não pode ser futura.");
        }

        private static bool CnpjValido(string cnpj)
        {
            // Esta é uma expressão regular simples para verificar se todos os caracteres são dígitos.
            var regex = new Regex(@"^\d{14}$");
            return regex.IsMatch(cnpj);
        }

        private static bool FormatoEnderecoValido(string endereco)
        {
            bool contemLetra = false;
            bool contemNumero = false;

            foreach (char c in endereco)
            {
                if (char.IsLetter(c)) contemLetra = true;
                if (char.IsDigit(c)) contemNumero = true;
            }

            return contemLetra && contemNumero;
        }
    }
}
