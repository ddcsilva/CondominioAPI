using CondominioAPI.Domain.Entities;

namespace CondominioAPI.Infrastructure.Repositories
{
    public class CondominioRepository : ICondominioRepository
    {
        private readonly List<Condominio> _condominios = new List<Condominio>();

        public async Task<IEnumerable<Condominio>> GetAllAsync()
        {
            return await Task.FromResult(_condominios.ToList());
        }

        public async Task<Condominio?> GetByIdAsync(Guid id)
        {
            var condominio = _condominios.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(condominio);
        }

        public async Task<Condominio> AddAsync(Condominio condominio)
        {
            _condominios.Add(condominio);
            return await Task.FromResult(condominio);
        }

        public async Task UpdateAsync(Condominio condominioToUpdate)
        {
            var index = _condominios.FindIndex(c => c.Id == condominioToUpdate.Id);
            if (index >= 0)
            {
                _condominios[index] = condominioToUpdate;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var condominio = _condominios.FirstOrDefault(c => c.Id == id);
            if (condominio != null)
            {
                _condominios.Remove(condominio);
            }
            await Task.CompletedTask;
        }

        public async Task<bool> AnyAsync()
        {
            return await Task.FromResult(_condominios.Any());
        }

        public async Task AddRangeAsync(IEnumerable<Condominio> condominios)
        {
            _condominios.AddRange(condominios);
            await Task.CompletedTask;
        }
    }
}
