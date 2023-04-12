using CondominioAPI.Domain.Entities;

namespace CondominioAPI.Domain.Services
{
    public interface ICondominioService
    {
        Task<IEnumerable<Condominio>> GetAllAsync();
        Task<Condominio?> GetByIdAsync(Guid id);
        Task<Condominio> AddAsync(Condominio condominio);
        Task UpdateAsync(Condominio condominio);
        Task DeleteAsync(Guid id);
    }
}
