using CondominioAPI.Domain.Entities;
using CondominioAPI.Infrastructure.Repositories;

namespace CondominioAPI.Application.Services
{
    public class CondominioService : ICondominioService
    {
        private readonly ICondominioRepository _repository;

        public CondominioService(ICondominioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<IEnumerable<Condominio>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Condominio?> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<Condominio> AddAsync(Condominio condominio)
        {
            return _repository.AddAsync(condominio);
        }

        public async Task UpdateAsync(Condominio condominio)
        {
            var existingCondominio = await _repository.GetByIdAsync(condominio.Id);
            if (existingCondominio != null)
            {
                existingCondominio.ApplyChanges(condominio);
                await _repository.UpdateAsync(existingCondominio);
            }
        }


        public Task DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
