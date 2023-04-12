using CondominioAPI.Domain.Entities;
using CondominioAPI.Domain.Repositories;
using CondominioAPI.Application.Services;
using Moq;

namespace CondominioAPI.Tests.Services
{
    public class CondominioServiceTests
    {
        private readonly Mock<ICondominioRepository> _mockRepository;
        private readonly CondominioService _service;

        public CondominioServiceTests()
        {
            _mockRepository = new Mock<ICondominioRepository>();
            _service = new CondominioService(_mockRepository.Object);
        }

        [Fact]
        public async Task ObterTodosCondominiosAssincrono_RetornaTodosOsCondominios()
        {
            // Arrange
            var condominios = new List<Condominio>
            {
                new Condominio
                {
                    Id = Guid.NewGuid(),
                    Nome = "Condominio A",
                    CNPJ = "00.000.000/0000-00",
                    Endereco = "Rua A, 123",
                    NumeroUnidades = 10,
                    NumeroBlocos = 2,
                    DataFundacao = DateTime.Now.AddYears(-5)
                },
                new Condominio
                {
                    Id = Guid.NewGuid(),
                    Nome = "Condominio B",
                    CNPJ = "11.111.111/1111-11",
                    Endereco = "Rua B, 456",
                    NumeroUnidades = 20,
                    NumeroBlocos = 4,
                    DataFundacao = DateTime.Now.AddYears(-10)
                }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(condominios);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(condominios, result);
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task AdicionarCondominioAssincrono_RetornaCondominioAdicionado()
        {
            // Arrange
            var condominio = new Condominio
            {
                Id = Guid.NewGuid(),
                Nome = "Condominio A",
                CNPJ = "00.000.000/0000-00",
                Endereco = "Rua A, 123",
                NumeroUnidades = 10,
                NumeroBlocos = 2,
                DataFundacao = DateTime.Now.AddYears(-5)
            };

            _mockRepository.Setup(repo => repo.AddAsync(condominio)).ReturnsAsync(condominio);

            // Act
            var result = await _service.AddAsync(condominio);

            // Assert
            Assert.Equal(condominio, result);
            _mockRepository.Verify(repo => repo.AddAsync(condominio), Times.Once());
        }


        [Fact]
        public async Task AtualizarCondominioAssincrono_RetornaCondominioAtualizado()
        {
            // Arrange
            var condominio = new Condominio
            {
                Id = Guid.NewGuid(),
                Nome = "Condominio A",
                CNPJ = "00.000.000/0000-00",
                Endereco = "Rua A, 123",
                NumeroUnidades = 10,
                NumeroBlocos = 2,
                DataFundacao = DateTime.Now.AddYears(-5)
            };

            _mockRepository.Setup(repo => repo.UpdateAsync(condominio)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(condominio);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(condominio), Times.Once());
        }

        [Fact]
        public async Task ExcluirCondominioAssincrono_RetornaCondominioExcluido()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once());
        }

    }
}
