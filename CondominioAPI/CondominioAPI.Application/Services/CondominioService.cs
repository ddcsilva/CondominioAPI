using CondominioAPI.Domain.Entities;
using CondominioAPI.Domain.Repositories;

namespace CondominioAPI.Application.Services
{
    public class CondominioService : ICondominioService
    {
        private readonly ICondominioRepository _repository;

        public CondominioService(ICondominioRepository repository)
        {
            _repository = repository;
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

        public Task UpdateAsync(Condominio condominio)
        {
            return _repository.UpdateAsync(condominio);
        }

        public Task DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}

