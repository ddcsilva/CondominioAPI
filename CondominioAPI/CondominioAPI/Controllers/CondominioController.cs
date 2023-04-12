using CondominioAPI.Application.Services;
using CondominioAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CondominioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondominioController : ControllerBase
    {
        private readonly ICondominioService _service;
        private readonly ILogger<CondominioController> _logger;

        public CondominioController(ICondominioService service, ILogger<CondominioController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Obter todos os condomínios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Condominio>>> GetCondominios()
        {
            _logger.LogInformation("Iniciando a busca por todos os condomínios.");

            try
            {
                var condominios = await _service.GetAllAsync();
                _logger.LogInformation("Busca por todos os condomínios concluída com sucesso.");
                return Ok(condominios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os condomínios.");
                throw;
            }
        }

        // Obter um condomínio específico pelo ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<Condominio>> GetCondominio(Guid id)
        {
            _logger.LogInformation($"Iniciando a busca pelo condomínio com ID {id}.");

            try
            {
                var condominio = await _service.GetByIdAsync(id);

                if (condominio == null)
                {
                    _logger.LogWarning($"Condomínio com ID {id} não encontrado.");
                    return NotFound();
                }

                _logger.LogInformation($"Condomínio com ID {id} encontrado com sucesso.");
                return Ok(condominio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o condomínio com ID {id}.");
                throw;
            }
        }

        // Criar um novo condomínio
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<Condominio>> CreateCondominio(Condominio condominio)
        {
            _logger.LogInformation("Iniciando a criação de um novo condomínio.");

            try
            {
                await _service.AddAsync(condominio);
                _logger.LogInformation($"Condomínio criado com sucesso. ID: {condominio.Id}.");
                return CreatedAtAction(nameof(GetCondominio), new { id = condominio.Id }, condominio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar um novo condomínio.");
                throw;
            }
        }

        // Atualizar um condomínio existente
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateCondominio(Guid id, Condominio condominio)
        {
            if (id != condominio.Id)
            {
                _logger.LogWarning($"Os IDs fornecidos não correspondem: {id} e {condominio.Id}.");
                return BadRequest();
            }

            _logger.LogInformation($"Iniciando a atualização do condomínio com ID {id}.");

            try
            {
                await _service.UpdateAsync(condominio);
                _logger.LogInformation($"Condomínio com ID {id} atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o condomínio com ID {id}.");
                throw;
            }
        }

        // Excluir um condomínio pelo ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteCondominio(Guid id)
        {
            _logger.LogInformation($"Iniciando a exclusão do condomínio com ID {id}.");

            try
            {
                var condominio = await _service.GetByIdAsync(id);

                if (condominio == null)
                {
                    _logger.LogWarning($"Condomínio com ID {id} não encontrado.");
                    return NotFound();
                }

                await _service.DeleteAsync(id);
                _logger.LogInformation($"Condomínio com ID {id} excluído com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir o condomínio com ID {id}.");
                throw;
            }
        }
    }
}