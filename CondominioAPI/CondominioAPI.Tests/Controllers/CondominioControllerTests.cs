using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using CondominioAPI.Domain.Entities;
using Xunit.Abstractions;
using CondominioAPI.Application.DTOs;

namespace CondominioAPI.Tests.Controllers
{
    public class CondominioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CondominioControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        [Fact]
        public async Task ObterCondominios_RetornaStatusCodeDeSucesso()
        {
            // Act
            var response = await _client.GetAsync("/api/condominio");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ObterCondominioPorId_RetornaStatusCodeDeSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/condominio/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AdicionarCondominio_RetornaStatusCodeDeCriado()
        {
            // Arrange
            var condominioDTO = new CondominioDTO
            {
                Nome = "Condomínio Exemplo",
                CNPJ = "12345678901234",
                Endereco = "Rua Exemplo, 123",
                NumeroUnidades = 10,
                DataFundacao = DateTime.Now.AddDays(-1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/condominio", condominioDTO);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }



        [Fact]
        public async Task AtualizarCondominio_RetornaStatusCodeDeSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var condominio = new Condominio
            {
                Nome = "Condominio A",
                CNPJ = "00.000.000/0000-00",
                Endereco = "Rua A, 123",
                NumeroUnidades = 10,
                NumeroBlocos = 2,
                DataFundacao = DateTime.Now.AddYears(-5)
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/condominio/{id}", condominio);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeletarCondominio_RetornaStatusCodeDeSemConteudo()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/condominio/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
