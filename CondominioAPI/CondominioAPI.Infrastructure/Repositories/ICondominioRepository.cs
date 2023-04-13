using CondominioAPI.Domain.Entities;

namespace CondominioAPI.Infrastructure.Repositories
{
    public interface ICondominioRepository
    {
        Task<IEnumerable<Condominio>> GetAllAsync();
        Task<Condominio?> GetByIdAsync(Guid id);
        Task<Condominio> AddAsync(Condominio condominio);
        Task UpdateAsync(Condominio condominioToUpdate);
        Task DeleteAsync(Guid id);
        Task<bool> AnyAsync();
        Task AddRangeAsync(IEnumerable<Condominio> condominios);
    }
}
