namespace CondominioAPI.Domain.Entities
{
    public class Condominio
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public int NumeroUnidades { get; set; }
        public int? NumeroBlocos { get; set; }
        public DateTime DataFundacao { get; set; }
    }
}
