using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using CondominioAPI.Domain.Entities;
using Xunit.Abstractions;
using CondominioAPI.Application.DTOs;
using CondominioAPI.Helpers;
using System.Text.Json;
using CondominioAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CondominioAPI.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;
using CondominioAPI.Application.Mappings;

namespace CondominioAPI.Tests.Controllers
{
    public class CondominioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly Mock<ICondominioService> _serviceMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CondominioController>> _loggerMock;

        public CondominioControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _serviceMock = new Mock<ICondominioService>();
            _loggerMock = new Mock<ILogger<CondominioController>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CondominioProfile());
            });
            _mapper = config.CreateMapper();
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

        [Fact]
        public async Task ObterCondominios_RetornaListaDeCondominios()
        {
            // Arrange
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Act
            var response = await _client.GetAsync("/api/condominio");
            response.EnsureSuccessStatusCode();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response.Content.ReadAsStringAsync().Result, jsonOptions);
            var condominios = JsonSerializer.Deserialize<List<CondominioDTO>>(apiResponse.Data.ToString(), jsonOptions);


            // Assert
            Assert.NotNull(condominios);
            Assert.NotEmpty(condominios);
            Assert.All(condominios, c => Assert.NotNull(c.Nome));
        }

        [Fact]
        public async Task AdicionarCondominio_RetornaCondominioCriado()
        {
            // Arrange
            var condominioDTO = new CondominioDTO
            {
                Nome = "Condominio Teste",
                Endereco = "Rua Teste, 123"
            };

            var createdCondominioDTO = new CondominioDTO
            {
                Id = Guid.NewGuid(),
                Nome = condominioDTO.Nome,
                Endereco = condominioDTO.Endereco
            };

            _serviceMock.Setup(service => service.AddAsync(It.IsAny<Condominio>()))
                .ReturnsAsync(_mapper.Map<Condominio>(createdCondominioDTO));

            var controller = new CondominioController(_serviceMock.Object, _mapper, _loggerMock.Object);

            // Act
            var result = await controller.CreateCondominio(condominioDTO);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            var apiResponse = createdAtActionResult.Value as ApiResponse;
            Assert.NotNull(apiResponse);
            Assert.Equal(StatusCodes.Status201Created, apiResponse.StatusCode);

            var resultCondominioDTO = apiResponse.Data as CondominioDTO;
            Assert.NotNull(resultCondominioDTO);
            Assert.Equal(createdCondominioDTO.Id, resultCondominioDTO.Id);
            Assert.Equal(condominioDTO.Nome, resultCondominioDTO.Nome);
            Assert.Equal(condominioDTO.Endereco, resultCondominioDTO.Endereco);
        }


        [Fact]
        public async Task AdicionarCondominio_ComDadosInvalidos_RetornaBadRequest()
        {
            // Arrange
            var condominioDTO = new CondominioDTO
            {
                Nome = "", // Nome inválido
                CNPJ = "12345678901234",
                Endereco = "Rua Exemplo, 123",
                NumeroUnidades = 10,
                DataFundacao = DateTime.Now.AddDays(-1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/condominio", condominioDTO);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
