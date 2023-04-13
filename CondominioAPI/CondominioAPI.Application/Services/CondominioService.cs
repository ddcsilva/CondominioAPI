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
            SeedFakeDataAsync().Wait();
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

        private async Task SeedFakeDataAsync()
        {
            if (!await _repository.AnyAsync())
            {
                var fakeCondominios = new List<Condominio>
                {
                    new Condominio { Id = Guid.NewGuid(), Nome = "Condomínio A", CNPJ = "11111111111111", Endereco = "Rua A, 123", NumeroUnidades = 10, NumeroBlocos = 2, DataFundacao = DateTime.Now.AddYears(-5) },
                    new Condominio { Id = Guid.NewGuid(), Nome = "Condomínio B", CNPJ = "22222222222222", Endereco = "Rua B, 456", NumeroUnidades = 20, NumeroBlocos = 4, DataFundacao = DateTime.Now.AddYears(-3) },
                };

                await _repository.AddRangeAsync(fakeCondominios);
            }
        }
    }
}
