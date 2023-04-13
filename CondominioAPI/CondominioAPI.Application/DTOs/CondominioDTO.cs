namespace CondominioAPI.Application.DTOs
{
    public class CondominioDTO
    {
        public CondominioDTO()
        {
            Nome = string.Empty;
            CNPJ = string.Empty;
            Endereco = string.Empty;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public int NumeroUnidades { get; set; }
        public int? NumeroBlocos { get; set; }
        public DateTime DataFundacao { get; set; }
    }
}
