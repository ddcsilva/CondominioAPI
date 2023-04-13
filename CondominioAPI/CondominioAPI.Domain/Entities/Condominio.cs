namespace CondominioAPI.Domain.Entities
{
    public class Condominio : BaseEntity
    {
        public Condominio()
        {
            Nome = string.Empty;
            CNPJ = string.Empty;
            Endereco = string.Empty;
        }

        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public int NumeroUnidades { get; set; }
        public int? NumeroBlocos { get; set; }
        public DateTime DataFundacao { get; set; }

        public void ApplyChanges(Condominio condominio)
        {
            Nome = condominio.Nome;
            CNPJ = condominio.CNPJ;
            Endereco = condominio.Endereco;
            NumeroUnidades = condominio.NumeroUnidades;
            NumeroBlocos = condominio.NumeroBlocos;
            DataFundacao = condominio.DataFundacao;
        }
    }
}
