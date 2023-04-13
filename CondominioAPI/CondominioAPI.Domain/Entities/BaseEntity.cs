namespace CondominioAPI.Domain.Entities
{
    /// <summary>
    /// Representa uma entidade básica do domínio com propriedades comuns a todas as entidades.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Obtém ou define o identificador único da entidade.
        /// </summary>
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
